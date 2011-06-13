<?php
echo '<ul>';
foreach ($products as $product)
{
    echo '<li>';
    
    echo '<h3>'.html::anchor('products/product/'.$product->id, text::limit_words($product->title,4, ' ...')).'</h3>';
    echo '<p>'.html::image('media/pictures/thumbs/'.$product->image).'</p>';
    echo '<p>Cena: '.$product->price.'zł</p>';
    echo '</li>';
}

echo '</ul>';

echo '<p>'.html::anchor('products/', 'Zobacz więcej...').'</p>';