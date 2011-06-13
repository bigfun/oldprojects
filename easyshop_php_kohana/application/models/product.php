<?php defined('SYSPATH') or die('No direct script access.');

class Product_Model extends ORM {

    protected $has_one = array('group');
    protected $has_many = array('comment');
	protected $sorting = array('id' => 'desc');

}
