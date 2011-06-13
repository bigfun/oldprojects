<?php defined('SYSPATH') or die('No direct script access.');

class Comment_Model extends ORM {

	protected $has_one = array('product', 'user');
	protected $sorting = array('id' => 'desc');

}
