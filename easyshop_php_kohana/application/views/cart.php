<?php

if ($cart)
{
echo '<ul>';
$counter = 5;
foreach ($cart as $id => $product)
{
    $session_cart = Session::instance()->get('cart');
    echo '<li>'.html::anchor('products/product/'.$id, text::limit_words($product->title,4,'...')).' Ilość:'.$session_cart[$id].'</li>';
    if (--$counter == 0)
        break;
}
echo '</ul>';
}
else
{
    echo '<p> Brak towarów w koszyku. '.html::anchor('products/', 'Dodaj').' towar!</p>';
}