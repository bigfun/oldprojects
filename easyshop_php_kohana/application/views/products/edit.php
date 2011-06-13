<?php
echo '<div class="post">';
echo '<div class="entry">';
echo '<h2 class="maintitle">Edycja produktu:</h2>';
echo '<p class="meta"><small>(Można używać znaczników html w opisie produktu)</small></p>';


echo form::open(NULL, array('enctype' => 'multipart/form-data'));

echo '<p>'.form::label('title', 'Nazwa produktu:').'<br / >'.form::input(array(
													 'name' => 'title',
													 'style' => 'width: 30em;',
													 'value' => $product->title)
													 ).'<br / >';

echo form::label('description', 'Opis produktu:').'<br / >'.form::textarea(array(
												'name' => 'description',
												'rows' => '20',
												'cols' => '50',
												'value' => $product->description)).'</p>';
echo '<p>'.form::label('price', 'Cena produktu:').'<br / >'.form::input(array(
													 'name' => 'price',
													 'style' => 'width: 10em;',
                                                     'value' => $product->price
                                                     )
													 ).'<br / >';
if ($product->image!=='')
{
    echo '<p> Obecny obrazek: </p>';
    echo html::file_anchor('media/pictures/'.$product->image,
                            html::image('media/pictures/thumbs/'.$product->image), array('rel'=>'lightbox')
                           );
}
else
{
    echo '<p> Brak obrazka produktu </p>';
}
echo '<p>'.form::label('image', 'Zmień obrazek produktu:').'<br / >'.form::upload(array(
													 'name' => 'image',
													 'class' => 'image')
													 ).'<br / >';
echo '<p>'.form::submit('submit', 'Zapisz produkt').'</p>';

echo form::close();
echo '</div>';
echo '</div>';

