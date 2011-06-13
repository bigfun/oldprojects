<?php
echo '<div class="products">';
		foreach ($products as $product)
		{
		   echo '<div class="product">';
		   echo '<h2 class="title">'.html::anchor('products/product/'.$product->id, $product->title).'</h2>';
           echo	'<p class="meta">Dodano dnia ', date('j.m.Y',$product->date),' o godzinie ',date('H:i:s',$product->date)  , '</p>';
           echo '<div class="image">';
           echo html::file_anchor('media/pictures/'.$product->image, html::image('media/pictures/thumbs/'.$product->image), array('rel'=>'lightbox'));
           echo '<p>kliknij obrazek,aby powiększyć</p>';
           echo '</div>';
		   echo '<div class="description">';
		   echo '<p>'.text::limit_words($product->description, 50, '...').'</p>';
		   echo '</div>';
           echo '<p class="price">Cena: <span>'.$product->price.' zł</span></p>';
           echo '<span class="readall">'.html::anchor('products/product/'.$product->id, " Pełny opis produktu ").'</span>';
		   echo '</div>';
		}
echo '</div>';
		echo $pages ;
