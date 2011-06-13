<?php
echo '<ul>';
foreach ($categories as $category => $value)
{
    list($count, $id) = split(' ',$value);
    if ((int)$count > 0)
    echo '<li>'.html::anchor('products/category/'.$id, $category).' ('.$count.') </li>';
}
echo '</ul>';