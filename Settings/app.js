(function () {

    return {

        events: {
            'app.activated': 'do'            
        },

        do: function () {
            alert(this.setting('subdomain'));
        }


        
    };

}());
