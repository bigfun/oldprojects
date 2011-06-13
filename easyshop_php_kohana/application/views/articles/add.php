<?php
echo '<h2 class="formtitle">Dodawanie nowego newsa</h2>';

if (!empty($errors)) {
    echo '<ul>';
    foreach ($errors as $error) {
        echo '<li>'.$error.'</li>';
    }
    echo '</ul>';
}

echo form::open(NULL,array('id' =>'newArticleForm', 'class' => 'easyform'));

echo form::open_fieldset();
echo '<p>'.form::label('title', 'Tytuł artykułu:').form::input('title').'</p>';

echo '<p>'.form::label('body', 'Treść artykułu:').form::textarea('body').'</p>';

echo '<p>'.form::submit('submit', 'Dodaj artykuł', ' class="submit"').'</p>';
echo form::close_fieldset();
echo form::close();



