$('#loadMore').on('click',
    'a.loadMorePosts',
    function (event) {
        event.preventDefault();
        var url = $('#loadMorePosts').attr('href');
        var data = {
            page: $('#loadMorePosts').attr('page')
        }
        $('#loadMore').empty();
        $.post(url, data,
            function (response) {
                DisplayProfileImages(response);
            });
    });

function DisplayProfileImages(data) {
    if (data.Items.length == 0) {
        $('#userPosts').prepend("<div class=\"emptySearch\">Upload your first photo</div>");
    } else {
        $.each(data.Items,
            function (index, value) {
                var imdiv = $('<div/>',
                    {
                        id: 'image',
                        class: 'col-lg-4 col-md-6'
                    }).appendTo('.userPosts');
                var link = $('<a />',
                    {
                        'href': '/Posts/ShowImage/' + value.Id,
                        'data-fancybox': 'images',
                        'data-type': 'image'
                    });
                link.append(
                    $('<img />').attr({
                        'src': '/Posts/ShowImage/' + value.Id,
                        'class':'viewImage'
                    })
                ).appendTo(imdiv);

                $('<div><a data-fancybox data-type="ajax" data-src="/Posts/PostDetails/' + value.Id
                        + '" href="/Posts/PostDetails/' + value.Id + '" class="viewDetails">View Details</a><div>')
                    .appendTo(imdiv);


            });

        if (data.PageInfo.TotalPages != data.PageInfo.PageNumber) {

            $('<a href="Profile/LoadMorePosts" id="loadMorePosts" class=" btn loadMorePosts signUpButton" page="' +
                data.PageInfo.PageNumber +
                '"> Load more</a>').appendTo('#loadMore');
        }
    }
}