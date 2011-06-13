<?php
echo '<div class="articles">';
echo '<div class="article">';
echo '<h2 class="maintitle">'.$article->title.'</h2>';
echo '<p class="meta">Napisał ', $article->user->name.' '.$article->user->surname,
' dnia '.date('j.m.Y', $article->date).' o godzinie '.date('H:i:s', $article->date).'. ';
if ($this->session->get('permission') == 'admin')
echo html::anchor('articles/edit/'.$article->id, 'Edytuj artykuł');
echo '</p>';
echo '<div class="text">'.$article->body.'</div>';
echo '</div>';
echo '</div>';

