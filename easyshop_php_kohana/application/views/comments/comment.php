<?php

echo '<div class="post">';
echo '<h2 class="maintitle">Komentarz nr '.$comment->id.' do produktu: '.$product.'</h2>';
echo '<p class="meta"><small>Dodany dnia '.date('j.m.Y', $comment->date).' o godzinie '.date('H:i:s', $comment->date).'. ';
if ($this->session->get('permission') == 'admin')
echo html::anchor('comments/edit/'.$comment->id, 'Edytuj komentarz');
echo '</a></small></p>';
echo '<div class="entry">'.$comment->body.'</div>';
echo '<div class="comments">';
echo '</div>';
echo '</div>';
