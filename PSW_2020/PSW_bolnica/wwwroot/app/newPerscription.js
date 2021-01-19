Vue.component("newPerscription", {
    data: function () {
        return {
            user: {
                username: UserData.username,
                role: UserData.role,
            },
            perscription: '',
            patientId: localStorage.getItem("patientId")
        }
    },
    template: `
   <div style="height: 81.7%;">
    <h1>Welcome <span style="color: seashell;">{{user.username}}</span></h1>
    <h2>Issue a perscription</h2>
        <div id="Div-panel">
            <form @submit.prevent="">
           <label class="label1" >perscription for a patient with id {{patientId}}:</label>
           <input style="width: 96%;" v-model="perscription" type="text" placeholder="perscription..." name="perscription" required> 

           <div id="center">
            <button class="button1" type="submit" v-on:click='perscribe()'>Submit</button> 
            <button class="button1" v-on:click='cancel()' > Cancel</button> 
        </div>
        </form>
        </div>
</div>
  
    `,
    methods: {
        perscribe() {
            axios
                .post('newPerscription/' + UserData.username + '/' + this.patientId + '/' + this.perscription)
                .then((responce) => this.succes(responce.data))
                .catch(() => this.failed());

        },
        succes(data) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'perscription has been saved',
                showConfirmButton: false,
                timer: 1400
            })
            this.$router.push('/perscriptions');
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