<?php defined('SYSPATH') or die('No direct script access.');

class Controller extends Controller_Core {
	public $template = 'template';
	public $auto_render = TRUE;
		
    public function __construct() {
        parent::__construct();
        header("Cache-Control: max-age=3600, must-revalidate");
        $this->session = Session::instance();    
		$this->template = new View($this->template);
		$this->template->title = 'easyshop';
        $id = $this->session->get('id', -1);
        $this->template->docutype='global';
        $this->template->formname = false;
        if ($this->_user()==='admin')
            {
                $this->template->admin = View::factory('admin');
            }
        else
        {
            $this->template->admin = false;
        }
		$this->template->side_login = View::factory('side_login')->set('user', $id===-1 ? 0 : ORM::factory('user')->find($id));
        $this->template->categories = View::factory('categories')->set('categories', $this->_generate_categories());
        $this->template->news = View::factory('news')->set('news',$this->_generate_news());
        $this->template->last_products = View::factory('last_products')->set('products',$this->_generate_last_products());
        $this->template->cart = View::factory('cart')->set('cart',$this->_generate_cart());
		if ($this->auto_render == TRUE) {
			Event::add('system.post_controller', array($this, '_render'));
		}
    }
    
    public function _user() {
        return $this->session->get('permission', 0);
    }
    
    public function _render() {
    	if ($this->auto_render == TRUE) {
    		$this->template->render(TRUE);
	    }
    }

    private function _generate_categories()
    {
        $categories = ORM::factory('group')->find_all();
        $ret = array();
        foreach ($categories as $category)
        {
            $products = ORM::factory('product')->where('group_id',$category->id);
            $products->find_all();
            $ret[$category->name] = $products->count_last_query().' '.$category->id;

        }
        return $ret;
    }
    protected function _generate_news()
    {
        $articles = ORM::factory('article')->limit(5)->find_all();
        return $articles;
    }
    protected function _generate_last_products()
    {
        $products = ORM::factory('product')->limit(5)->find_all();
        return $products;
    }

    protected function _generate_cart()
    {
        $ret = array();
        if (is_array($this->session->get('cart')))
        {
            $products = $this->session->get('cart');
            $query = ORM::factory('product');
            foreach ($products as $id => $count)
           {
               $ret[$id] = $query->find($id);
           }
        }
        return $ret;
    }

}
