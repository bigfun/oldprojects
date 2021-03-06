<?php

class Articles_Controller extends Controller
{
    public function __construct()
    {
        parent::__construct();
        $this->template->title = "easyshop - Newsy";
    }
    public function index()
    {
        $this->view();
    }

    public function view($page_num = 1)
    {


        $per_page = $this->input->get('limit', 5);
        $offset = ($page_num-1)*$per_page;

        $query = ORM::factory('article');
        $articles = $query->limit($per_page, $offset)->find_all();
        $pages = Pagination::factory( array
        (
        'style'=>'digg',
        'base_url'=> 'articles/view',
        'items_per_page'=>$per_page,
        'query_string'=>'',
        'total_items'=>$query->count_last_query()
        ));

        $this->template->content = View::factory('articles/view');
        $this->template->content->articles = $articles;
        $this->template->content->pages = $pages;

    }

    public function article($id)
    {
        $query = ORM::factory("article")->find($id);
        if ($query->count_last_query() < 1)
        {
            $this->template->title .= ' - nie znaleziono artykułu';
            $this->template->content = View::factory('articles/notexist');
        }
        else
        {
            $this->template->title .= ' - '.$query->title;
            $this->template->content = View::factory('articles/article')->set('article', ORM::factory('article')->find($id));
        }
    }

    public function add()
    {
        $this->template->docutype ='form';
        $this->template->formname ='newarticleform';
        if ($this->_user() !== 'admin')
        {
            url::redirect();
            return;
        }

        $_POST = Validation::factory($_POST)
        ->add_rules('title', 'required', 'length[1,250]')
        ->add_rules('body', 'required');
        if ($_POST->validate())
        {
            $article = ORM::factory('article');
            $article->title = $_POST['title'];
            $article->body = $_POST['body'];
            $article->user_id = $this->session->get("id");
            $article->date = time();
            $article->save();
            $this->session->set_flash('user_message', 'Dodano artykuł do bazy danych');
            url::redirect('articles/article/'.$article->id);
        } else
        {
            $this->template->title .= ' - Dodaj nowy artykuł';
            $this->template->content = View::factory('articles/add')->set('errors', $_POST->errors('validation'));
        }
    }

    public function edit($id)
    {

        $this->template->docutype ='form';
        $this->template->formname ='newarticleform';
        if ($this->_user() !== 'admin')
        {
            url::redirect();
            return;
        }
        $article = ORM::factory('article')->find($id);
        if ($article->count_last_query() < 1)
        {
            $this->session->set_flash('user_message', 'Nie ma takiego newsa w bazie, dodajesz nowy.');
            url::redirect('articles/add');
        }
        else
        {


            $_POST = Validation::factory($_POST)
            ->add_rules('title', 'required', 'length[1,250]')
            ->add_rules('body', 'required');
            if ($_POST->validate())
            {
                $article = ORM::factory('article', $id);
                $article->title = $_POST['title'];
                $article->body = $_POST['body'];
                $article->user_id = $this->session->get("id");
                $article->date = time();
                $article->save($id);
                $this->session->set_flash('user_message', 'Zapisano edycję do bazy danych');
                url::redirect('articles/article/'.$id);
            } else
            {
                $this->template->title .= ' - Edycja artykułu: '.$article->title;
                $this->template->content = View::factory('articles/edit')->set(array('article' => $article,'errors' =>$_POST->errors('validation') ));
            }
        }
    }


    	public function __call($method, $arguments)
	{
		// Disable auto-rendering
		$this->auto_render = FALSE;

		// By defining a __call method, all pages routed to this controller
		// that result in 404 errors will be handled by this method, instead of
		// being displayed as "Page Not Found" errors.
		echo 'This text is generated by __call. If you expected the index page, you need to use: welcome/index/'.substr(Router::$current_uri, 8);
	}

}

