<?php

class Cart_Controller extends Controller
{

    public function __construct()
    {
        parent::__construct();
        $this->template->title = "easyshop - Koszyk";
    }
    public function index()
    {
        url::redirect('cart/view');
    }
    public function view()
    {
        $products = array();
        $cart = $this->_generate_cart();
        if ($cart)
        {
            $query = ORM::factory('product');

            foreach ($cart as $id => $count)
            {
                $product = $query->find($id);
                $products[$id] = $product;
            }
        }
        $this->template->content = View::factory('cart/view');
        $this->template->content->products = $products;


    }

    public function add($id)
    {
        if (!is_array($this->session->get('cart')))
        {
            $this->session->set('cart',array());
        }
        $cart = $this->session->get('cart');
        $query = ORM::factory('product');
        $query->find($id);
        if ($query->count_last_query() < 1)
        {
            $this->session->set_flash('user_message', 'Brak towaru w bazie danych.');
            url::redirect('products');
        }
        if (isset($cart[$id]) and is_numeric($cart[$id]))
        $cart[$id]++;
        else
        $cart[$id] = 1;
        $this->session->set('cart',$cart);
        $this->session->set_flash('user_message', 'Dodano towar do koszyka.');
        url::redirect('products/product/'.$id);
    }

    public function remove($id)
    {
        if (!is_array($this->session->get('cart')))
        {
            return;
        }
        $cart = $this->session->get('cart');
        if (isset($cart[$id]) and is_numeric($cart[$id]))
        {
            if ($cart[$id] > 1)
                $cart[$id]--;
            else
                unset($cart[$id]);
        }
        $this->session->set('cart',$cart);
        url::redirect('cart/view');

    }
    public function removeall($id)
    {
        if (!is_array($this->session->get('cart')))
        {
            return;
        }
        $cart = $this->session->get('cart');
        if (isset($cart[$id]))
        {
            unset($cart[$id]);
        }
        $this->session->set('cart',$cart);
        url::redirect('cart/view');
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
