var plugin = 
{
    OpenNewTab : function(url)
    {
       url = UTF8ToString(url);
        window.open(url, '_blank');
    }
};

var plugin2 = 
{
    OpenInCurrentTab : function(url)
    {
        url = UTF8ToString(url);
        window.open(url, '_self');
    }
}; 
    

mergeInto(LibraryManager.library, plugin);
mergeInto(LibraryManager.library, plugin2);
