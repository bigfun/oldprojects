<?php

class Products_Controller extends Controller
{   


	public function __construct()
    {
        parent::__construct();
        $this->template->title = "easyshop - Produkty";
    }
    public function index()
    {
        url::redirect('products/view');
    }

    public function view($page_num = 1)
    {
        $per_page = $this->input->get('limit', 5);
        $offset = ($page_num-1)*$per_page;

        $query = ORM::factory('product');
        $products = $query->limit($per_page, $offset)->find_all();
        $pages = Pagination::factory( array
        (
        'style'=>'digg',
        'items_per_page'=>$per_page,
        'query_string'=>'',
        'total_items'=>$query->count_last_query()
        ));

        $this->template->content = View::factory('products/view');
        $this->template->content->products = $products;
        $this->template->content->pages = $pages;

    }

    public function product($id)
    {
        $query = ORM::factory("product")->find($id);
        if ($query->count_last_query() < 1)
        {
            $this->template->title .= ' - nie znaleziono produktu';
            $this->template->content = View::factory('products/notexist');
        }
        else
        {
            $comments = ORM::factory('comment')->where('product_id', $id)->find_all();
            $this->template->title .= ' - '.$query->title;
            $this->template->content = View::factory('products/product')->set(array(
                                                                           'product' => ORM::factory('product')->find($id),
                                                                           'comments' => $comments)
                                                                          );
        }
    }

    public function add()
    {
        $this->template->docutype='form';
        $this->template->formname ='newproductform';
        if ($this->_user() !== 'admin')
        {
            url::redirect();
            return;
        }
		$files = Validation::factory($_FILES)
		->add_rules('image', 'upload::valid', 'upload::type[gif,jpg,png]', 'upload::size[2M]');

        $_POST = Validation::factory($_POST)
        ->add_rules('title', 'required', 'length[1,250]')
        ->add_rules('description', 'required')
		->add_rules('price', 'required');
        if ($_POST->validate() and $files->validate())
        {   
			$filename = upload::save('image');
            $savename = basename($filename).time().'.jpg';
			Image::factory($filename)
			->resize(100, 100)
			->save(DOCROOT.'media/pictures/thumbs/'.$savename);
            Image::factory($filename)->save(DOCROOT.'media/pictures/'.$savename);
			unlink($filename);
            $product = ORM::factory('product');
            $product->title = $_POST['title'];
            $product->description = $_POST['description'];
			$product->price = str_replace(',','.',$_POST['price']);
            $product->date = time();
            $product->group_id = $_POST['category'];
			$product->image = $savename;
            $product->save();
            $this->session->set_flash('user_message', 'Dodano produkt do bazy danych');
            url::redirect('products/product/'.$product->id);
        } else
        {
            $this->template->title .= ' - Dodaj nowy produkt';
            $this->template->content = View::factory('products/add')->set(array(
                                                                    'errors' => $_POST->errors('validation'),
                                                                    'errors2' => $files->errors('validation'),
                                                                    'categories' => ORM::factory('group')->find_all()
                                                                                ));
        }
    }
    public function edit($id)
    {

        if ($this->_user() !== 'admin')
        {
            url::redirect();
            return;
        }
        $product = ORM::factory('product')->find($id);
        if ($product->count_last_query() < 1)
        {
            $this->session->set_flash('user_message', 'Nie ma takiego produktu w bazie, dodajesz nowy.');
            url::redirect('products/add');
        }
        else
        {
		$files = Validation::factory($_FILES)
		->add_rules('image', 'upload::valid', 'upload::type[gif,jpg,png]', 'upload::size[2M]');

        $_POST = Validation::factory($_POST)
        ->add_rules('title', 'required', 'length[1,250]')
        ->add_rules('description', 'required')
		->add_rules('price', 'required');
        if ($_POST->validate() and $files->validate())
        {
            $product = ORM::factory('product', $id);
            $product->title = $_POST['title'];
            $product->description = $_POST['description'];
			$product->price = $_POST['price'];
            $product->date = time();
            if (isset($_FILES['image']) and $_FILES['image']['name'] !=='')
            {
			$filename = upload::save('image');
            $savename = basename($filename).time().'.jpg';
			Image::factory($filename)
			->resize(100, 100, Image::WIDTH)
			->save(DOCROOT.'media/pictures/thumbs/'.$savename);
            Image::factory($filename)->save(DOCROOT.'media/pictures/'.$savename);
			unlink($filename);
            if ($product->image != '')
            {
                unlink(DOCROOT.'media/pictures/thumbs/'.$product->image);
                unlink(DOCROOT.'media/pictures/'.$product->image);
            }
            $product->image = $savename;
            }
            $product->save($product->id);
            $this->session->set_flash('user_message', 'Zapisano zmiany do bazy danych');
           url::redirect('products/product/'.$product->id);
        } else
        {
            $this->template->title .= ' - Edycja produktu: '.$product->title;;
            $this->template->content = View::factory('products/edit')->set('product', $product);
        }
        }
    }

    public function addcategory($name)
    {
            $this->template->auto_render = FALSE;
            $this->auto_render = false;
        if (request::is_ajax())
        {

            if ($this->_user() !=='admin' or !is_string($name) or $name ==='' )
            {
                echo "<?xml version=\"1.0\"?>\n";
                echo "<response>\n";
                echo "\t<status>1</status>\n";
                echo "</response>\n";
            }
            else
            {
                $query = ORM::factory('group');
                if ($query->where('name', $name)->find()->count_last_query() < 1)
                {
                    $query->name = $name;
                    $query->save();
                }
                $categories = $query->find_all();
                echo "<?xml version=\"1.0\"?>\n";
                echo "<response>\n";
                echo "\t<status>2</status>\n";
                echo "\t<categories>\n";
                foreach ($categories as $category)
                {
                    echo "\t\t<category>\n";
                    echo "\t\t\t<id>".$category->id."</id>\n";
                    echo "\t\t\t<name>".$category->name."</name>\n";
                    echo "\t\t</category>\n";
                }
                echo "\t</categories>\n";
                echo "</response>\n";
            }
        }

    }
    public function category($id, $page_num=1)
    {
        $this->template->title.= ' - Kategoria: '.ORM::factory('group',$id)->find()->name.' - strona '.$page_num;
        $per_page = $this->input->get('limit', 5);
        $offset = ($page_num-1)*$per_page;

        $query = ORM::factory('product');
        $products = $query->where('group_id',$id)->limit($per_page, $offset)->find_all();
        $pages = Pagination::factory( array
            (
        'style'=>'digg',
        'items_per_page'=>$per_page,
        'base_url'=> 'products/category',
        'uri_segment' => $id,
        'total_items'=>$query->count_last_query()
            ));

        $this->template->content = View::factory('products/view');
        $this->template->content->products = $products;
        $this->template->content->pages = $pages;
    }

    public function search($string = '', $page_num = 1)
    {
         
        if ($string == '')
        {
            $string = $this->input->get('string');
            if ($string === '')
            {
                url::redirect('products/');
                return;
            }
        }
        $this->template->title.= ' - Szukaj produktu: '.$string.' - strona '.$page_num;
        $per_page = $this->input->get('limit', 5);
        $offset = ($page_num-1)*$per_page;

        $query = ORM::factory('product');
        $products = $query->like('title',$string)->limit($per_page, $offset)->find_all();
        $pages = Pagination::factory( array
            (
        'style'=>'digg',
        'items_per_page'=>$per_page,
        'base_url'=> 'products/search',
        'query_string'=>'',
        'uri_segment' => $string,
        'total_items'=>$query->count_last_query()
            ));

        $this->template->content = View::factory('products/view');
        $this->template->content->products = $products;
        $this->template->content->pages = $pages;
    }

}
