<?php
echo '<div class="post">';

echo '<h2 class="title"> Koszyk </h2>';
if ($products)
{
    echo '<table class="cart">';
    echo '<tr><th>Nazwa:</th><th>Ilość:</th><th>Cena (zł):</th><th>Razem:</th></tr>';
    foreach ($products as $product)
    {
        echo '<tr>';
        echo '<td>'.html::anchor('products/product/'.$product->id, text::limit_words($product->title,10,'...')).'</td>';
        echo '<td>'.$_SESSION['cart'][$product->id].'</td>';
        echo '<td>'.$product->price.'</td>';
        echo '<td>'.$_SESSION['cart'][$product->id]*$product->price.'</td>';
        echo '<td>'.html::anchor('cart/remove/'.$product->id, 'Usuń').'</td>';
        if ($_SESSION['cart'][$product->id] > 1)
        echo '<td>'.html::anchor('cart/removeall/'.$product->id, 'Usuń wszystkie').'</td>';
        echo '</tr>';

    }
    echo '</table>';
}
else
{
    echo '<p>Brak towarów w koszyku.</p>';
}
echo '</div>';
