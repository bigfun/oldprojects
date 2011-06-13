<?php
echo '<div class="post">';
echo '<div class="entry">';
echo '<h2 class="maintitle">Edycja komentarza nr '.$comment->id.'</h2>';


echo form::open();


echo form::label('body', 'Treść komentarza:').'<br / >'.form::textarea(array(
												'name' => 'body',
												'rows' => '20',
												'cols' => '50',
												'value' => $comment->body)).'</p>';
echo '<p>'.form::submit('submit', 'Zapisz komentarz').'</p>';

echo form::close();
echo '</div>';
echo '</div>';
