<?php
echo '<ul>';
foreach ($news as $new)
{
    echo '<li>'.html::anchor('articles/article/'.$new->id, text::limit_words($new->title,4, ' ...'));
}
echo '</ul>';