<?php defined('SYSPATH') or die('No direct script access.');

class User_Model extends ORM {

//protected $has_many = array('article','comment');
protected $has_one = array('contact');
    public function __get($key) {     
        if ($key == 'permission') {
            return $this->object[$key] == 2 ? 'admin' : 'klient';
        }
        return parent::__get($key);
    }
}
