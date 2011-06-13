<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="content-type" content="text/html; charset=utf-8" />
        <title><?php echo $title; ?></title>
        <meta name="keywords" content="shop sklep zakupy ksiazki" />
        <meta name="description" content="" />
        <?php echo html::script('media/js/jquery'); ?>
        <?php echo html::script('media/js/jquery-ui'); ?>
        <?php echo html::script('media/js/jquery.selectboxes'); ?>
        <?php echo html::script('media/js/lightbox'); ?>
        <?php if ($docutype === 'form')
        {
            echo html::script('media/js/jquery.validate');
            echo html::script('media/js/jquery.maskedinput');
            echo html::script('media/js/'.$formname);
        }

        ?>
        <?php echo html::stylesheet('media/css/default'); ?>

        <?php echo html::stylesheet('media/css/lightbox'); ?>
        <?php echo html::stylesheet('media/theme/ui.all'); ?>
    </head>
    <body>
        <div id="header">
            <div id="logo">
                <h1><a href="<?php echo url::base();?>">EasyShop</a></h1>
                <h2>Twoje Miejsce na zakupy!</h2>
            </div>
            <?php if ($admin) echo $admin; ?>
            <div id="login">
                <?php echo $side_login ;?>
            </div>
        </div>
        <div id="page">
            <div id="sidebar1" class="sidebar">
                <ul>
                    <li id="search">
                        <h2>Szukaj produktu:</h2>
                        <?php
                        echo form::open('products/search', array('id' => 'searchform', 'method' => 'get'));
                        echo '<div>';
                        echo form::input(array(
                                                                'name' => 'string',
                                                                'id' => 's',
                                                                'size' => '15'
                            )
                        );
                        echo form::submit(NULL, 'Szukaj');
                        echo '</div>';
                        echo form::close();
                        ?>
                    </li>
                    <li id="cart">
                        <h2><?php echo html::anchor('cart/view','Twój koszyk:'); ?></h2>
                        <?php echo $cart; ?>

                    </li>
                    <li id="categories">
                        <h2>Kategorie:</h2>
                        <?php echo $categories; ?>
                    </li>
                    <li id="news">
                        <h2>Newsy:</h2>
                        <?php echo $news; ?>
                    </li>
                </ul>
            </div>
            <div id="content">
                <?php if ($this->session->get('user_message')): ?>
                <div class="user_message"><?php echo $this->session->get('user_message'); ?></div>
                <?php endif; ?>
                <?php echo $content; ?>
            </div>
            <div id="sidebar2" class="sidebar">
                <ul>
                    <li id="recent-products">
                        <h2><?php echo html::anchor('products/','Ostatnio dodane:');?></h2>
                        <ul>

                            <?php echo $last_products; ?>

                        </ul>
                    </li>
                </ul>
            </div>
            <div style="clear: both;">&nbsp;</div>
        </div>
        <div id="footer">
            <p class="legal">&copy;2009 By Bigfun</p>
            <p class="credit">Design by Tysia & Iwcia</p>
        </div>
    </body>
</html>
