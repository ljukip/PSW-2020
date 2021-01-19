Vue.component("home", {
  data: function () {
    return {
      review: {},
      reviews: []
    }
  },
  template: `

      <div>
        <header class="App-header">
          <button class="button1" style="display: inline-block;""@click="$router.push('/login') ">Login</button>
          <img src="images/logo.png" class="App-logo" alt="logo" />
            <button class="button1" style="display: inline-block;""@click="$router.push('/registration') ">Registration</button>
        </header>
        <div>
                <table class="myTable">
                    <thead>
                        <tr class="header">
                            <th>reviews</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='review.value' v-for='review in reviews'>
                            <td v-if="review.isPublished">{{review.text}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
      </div>`,
  methods: {

  },
  created() {
    if (UserData != null) {
      this.$router.push('/homeUser');
    }
    axios
      .get('getFeedbacks')
      .then(Response => (this.reviews = Response.data));
  },
});
