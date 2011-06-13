<?php
echo '<div class="articles">';
		foreach ($articles as $article)
		{
		   echo '<div class="article">';
		   
		   echo '<h2 class="title">'.html::anchor('articles/article/'.$article->id, $article->title).'</h2>';
		   echo	'<p class="meta">Napisał ',$article->user->name.' '.$article->user->surname, ' dnia ', date('j.m.Y',$article->date),' o godzinie ',date('H:i:s',$article->date)  , ' </a></p>';
		   echo '<div class="text">';
		   echo text::limit_words($article->body, 50, '...');
		   echo '</div>';
           echo '<span class="readall">'.html::anchor('articles/article/'.$article->id, " Czytaj całość ").'</span>';
		   echo '</div>';
		}
echo '</div>';
		echo $pages ;

