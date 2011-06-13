<?php
echo '<div class="products">';
echo '<div class="productone">';
echo '<h2 class="maintitle">'.$product->title.'</h2>';
echo '<p class="meta">Dodany dnia '.date('j.m.Y', $product->date).' o godzinie '.date('H:i:s', $product->date).'. ';
if ($this->session->get('permission') == 'admin')
echo html::anchor('products/edit/'.$product->id, 'Edytuj produkt');
echo '</p>';
echo '<p class="priceone">Cena: <span>'.$product->price.' zł</span></p>';
echo '<div class="image">';
echo html::file_anchor('media/pictures/'.$product->image, html::image('media/pictures/thumbs/'.$product->image), array('rel'=>'lightbox'));
echo '<p>kliknij obrazek,aby powiększyć</p>';
echo '</div>';
echo '<div class="addtocart">';
echo html::anchor('cart/add/'.$product->id,html::image('media/images/addtocart.gif', array('alt' => 'Dodaj do koszyka', 'class' => 'no_border')));
echo '</div>';
echo '<div class="description">';
echo '<p>'.$product->description.'</p>';
echo '</div>';
echo '</div>';
echo '<div class="comments">';
echo '<h3 class="title">Komentarze:</h3>';
if (count($comments) > 0)
{
    foreach ($comments as $comment)
    {
        echo '<div class="comment">';
        $user = ORM::factory('user')->find($comment->user_id);
        echo '<p class="author">'.$user->name.' '.$user->surname.' napisał:</p>';
        if (Session::instance()->get('permission') === 'admin' or $comment->user_id === Session::instance()->get('id'))
        echo '<span class="edit">'.html::anchor('comments/edit/'.$comment->id, 'Edytuj komentarz').'</span>';
        echo '<div class="body"><p>'.$comment->body.'</p></div>';
        echo '</div>';
    }
}
else
{
    echo '<h3>Produkt nie posiada komentarzy</h3>';
}
echo '</div>';
$user_id = Session::instance()->get('id', -1);
if ($user_id > 0)
{
    $checker = ORM::factory('comment')->where(array('user_id' => $user_id, 'product_id'=> $product->id))->find();
    if ($checker->count_last_query() < 1)
    {
        echo '<div class="newcomment">';
        echo '<h3>Dodaj komentarz do produktu:</h3>';

        echo form::open('comments/add/'.$product->id);

        echo form::label('body', 'Treść komentarza:').'<br / >'.form::textarea(array(
                                                'name' => 'body',
                                                'rows' => '20',
                                                'cols' => '50')).'</p>';

        echo '<p>'.form::submit('submit', 'Dodaj komentarz').'</p>';

        echo form::close();
        echo '</div>';
    }
    else
    {
        echo '<h3 class="commentinfo">Już dodałeś komentarz do tego produktu</h3>';
    }
}
else
{
    echo '<h3 class="commentinfo">Aby dodawać komentarze, musisz być zalogowany. '.html::anchor('admin/login', 'Zaloguj się.').'</h3>';
}

echo '</div>';
