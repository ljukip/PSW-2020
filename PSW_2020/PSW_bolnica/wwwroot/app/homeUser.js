Vue.component("homeUser", {
    data: function () {
        return {
            user: {
                username: localStorage.getItem('username'),
                role: localStorage.getItem('role')
            }
        }
    },
    template: `
   <div style="height: 81.7%;">
    <h1>Welcome <span style="color: seashell;">{{user.username}}</span></h1>
        <div id="Div-panel">
            <div style="flex-direction: row;">
                    <button class="oval" style="background-image: url('images/profileOval.png');display: inline-block;"@click="$router.push('/profileUser')"><p></p> </button>
                    <button v-if="user.role ==='ADMIN'" class="oval" style="background-image: url('images/feedback.png');display: inline-block;"@click="$router.push('/') "><p></p></button>
                    <button v-if="user.role ==='PATIENT'" class="oval" style="background-image: url('images/appointment.jpg');display: inline-block;"@click="$router.push('/') "><p></p></button>
                    <button v-if="user.role ==='ADMIN'" class="oval" style="background-image: url('images/users.jpg');display: inline-block;"@click="$router.push('/listUsers')  "><p> </p></button>
                    <button v-if="user.role ==='PATIENT'" class="oval" style="background-image: url('images/records.jpg');display: inline-block;"@click="$router.push('/') "><p> </p></button>
            </div>
            <div style="flex-direction: row;">
                <router-link style="width: 40%;" to='/profileUser'>profile</router-link>
                <router-link  style="width: 20%;" v-if="user.role ==='PATIENT'"  to='/'>new appointment</router-link>
                <router-link  style="width: 20%;" v-if="user.role ==='ADMIN'" to='/'>feedback</router-link>
                <router-link v-if="user.role ==='PATIENT'" style="width: 38%;" to='/'>medical records</router-link>
                <router-link v-if="user.role ==='ADMIN'" style="width: 38%;" to='/listUsers'>users</router-link>
            </div>
        </div>
</div>
  
    `,
    methods: {

    }
})