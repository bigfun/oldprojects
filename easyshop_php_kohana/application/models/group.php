<?php defined('SYSPATH') or die('No direct script access.');

class Group_Model extends ORM {

	protected $has_many = array('product');
	protected $sorting = array('name' => 'asc');

}
