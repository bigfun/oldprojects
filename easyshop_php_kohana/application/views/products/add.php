<?php
echo '<h2 class="formtitle">Dodawanie nowego produktu</h2>';

if (!empty($errors)) {
    echo '<ul>';
    foreach ($errors as $error) {
        echo '<li>'.$error.'</li>';
    }
    echo '</ul>';
}
if (!empty($errors2)) {
    echo '<ul>';
    foreach ($errors2 as $error) {
        echo '<li>'.$error.'</li>';
    }
    echo '</ul>';
}


echo form::open(NULL, array('enctype' => 'multipart/form-data','id' =>'newProductForm', 'class' => 'easyform'));

echo '<p>'.form::label('title', 'Nazwa produktu:').form::input('title').'</p>';

echo '<p>'.form::label('description', 'Opis produktu:').form::textarea('description').'</p>';
												
echo '<p>'.form::label('price', 'Cena produktu:').form::input('price').'</p>';
echo '<p>'.form::label('image', 'Obrazek produktu:').form::upload(array(
													 'name' => 'image',
													 'class' => 'image')).'</p>';
$selection = array();
foreach ($categories as $category)
{
    $selection[$category->id] = $category->name;
}
echo '<p>'.form::label('category', 'Kategoria produktu:').form::dropdown('category',$selection).'  '.'<span id="categoryloader"></span></p>';
echo html::anchor('#', '<span class="ui-icon ui-icon-newwin"></span>Dodaj kategorię',array('id' => 'add_category_link', 'class' =>'ui-state-default ui-corner-all'));
echo '<p>'.form::submit('submit', 'Dodaj produkt',' class="submit"').'</p>';

echo form::close();
echo '<div id="add_category_dialog" title="Dodaj lategorię">';
echo form::open(NULL, array('id' =>'newCategoryForm', 'class' => 'easyform'));
echo '<p>'.form::label('categorytitle', 'Nazwa kategorii:').form::input('categorytitle').'</p>';
echo form::close();
echo '</div>';

