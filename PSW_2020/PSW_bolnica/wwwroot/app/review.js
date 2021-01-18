Vue.component("review", {
    data: function () {
        return {
            user: {
                username: localStorage.getItem('username'),
                role: localStorage.getItem('role'),
            },
            review: '',
        }
    },
    template: `
   <div style="height: 81.7%;">
    <h1>Welcome <span style="color: seashell;">{{user.username}}</span></h1>
    <h2>we care about your oppinion...</h2>
        <div id="Div-panel">
            <form>
           <label class="label1" >Leave a comment:</label>
           <input style="width: 96%;" v-model="review" type="text" placeholder="Leve a comment" name="comment" required> 

           <div id="center">
            <button class="button1" type="submit" v-on:click='comment()'>Submit</button> 
            <button class="button1" v-on:click='cancel()' > Cancel</button> 
        </div>
        </form>
        </div>
</div>
  
    `,
    methods: {
        comment() {
            axios
                .post('newFeedback/' + UserData.username + '/' + this.review)
                .then((responce) => this.succes(responce.data))
                .catch(() => this.failed());

        },
        succes(data) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Feedback has been saved',
                showConfirmButton: false,
                timer: 1400
            })
            this.$router.push('/appointments');
        },
        failed() {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            })
        }
    },
    created() {


        if (UserData == {}) {
            this.$router.push('/login');

        }
    },
})