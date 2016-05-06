$(document).ready(function(){
$(".ui.dropdown").dropdown();
$('.activating.element').popup();
$('.example input')
  .popup({
    on: 'focus'
  })
;
$('.button')
  .popup({
    inline: true
  })
;
$('.dynamic.example .menu .item')
  .tab({
    context : '.dynamic.example',
    auto    : true,
    path    : '/'
  })
;
$('.menu .item')
  .tab()
;
$('.history.example .menu .item')
  .tab({
    context : '.history.example',
    history : true
  })
;
$('.ui.checkbox')
  .checkbox()
;
$('.ui.form')
  .form({
    name: {
      identifier  : 'name',
      rules: [
        {
          type   : 'empty',
          prompt : 'Please enter your name'
        }
      ]
    },
    email: {
      identifier  : 'email',
      rules: [
            {
              type   : 'empty',
              prompt : 'Please enter an e-mail'
            },
            {
              type   : 'email',
              prompt : 'Please enter a valid e-mail'
            }

      ]
    },
    gender: {
      identifier  : 'gender',
      rules: [
        {
          type   : 'empty',
          prompt : 'Please select a gender'
        }
      ]
    },
    city: {
      identifier  : 'city',
      rules: [
        {
          type   : 'empty',
          prompt : 'Please select a city'
        }
      ]
    },
    service: {
      identifier  : 'service',
      rules: [
        {
          type   : 'empty',
          prompt : 'Please select a service'
        }
      ]
    },
    username: {
      identifier : 'username',
      rules: [
        {
          type   : 'empty',
          prompt : 'Please enter a username'
        }
      ]
    },
    password: {
      identifier : 'password',
      rules: [
        {
          type   : 'empty',
          prompt : 'Please enter a password'
        },
        {
          type   : 'length[6]',
          prompt : 'Your password must be at least 6 characters'
        }
      ]
    },
    terms: {
      identifier : 'terms',
      rules: [
        {
          type   : 'checked',
          prompt : 'You must agree to the terms and conditions'
        }
      ]
    }
  })
;
$('.message .close').on('click', function() {
  $(this).closest('.message').fadeOut();
});

});