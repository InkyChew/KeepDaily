export const environment = {
  production: true,
  api: {
    // url: 'http://localhost:9001/api',
    url: 'https://keepdaily.azurewebsites.net/api',
    endpoint: {
      user: '/User',
      friend: '/Friend',
      plan: '/Plan',
      category: '/Category',
      day: '/Day',
      lineNotify: '/LineNotify',
      oAuthGoogle: '/OAuthGoogle',
      confirmEmail: '/ConfirmEmail',
      message: '/Message'
    }
  },
  hub: {
    url: 'https://keepdaily.azurewebsites.net/api',
    endpoint: {
      messageHub: '/MessageHub'
    }
  }
};
