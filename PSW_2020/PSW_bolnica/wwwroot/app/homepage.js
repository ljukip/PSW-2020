Vue.component("home", {
  template: `

        <header class="App-header">
          <button class="button1" style="display: inline-block;""@click="$router.push('/login') ">Login</button>
          <img src="images/logo.png" class="App-logo" alt="logo" />
            <button class="button1" style="display: inline-block;""@click="$router.push('/registration') ">Registration</button>
        </header>`,
});
