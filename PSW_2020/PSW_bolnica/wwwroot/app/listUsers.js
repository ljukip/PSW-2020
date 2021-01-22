Vue.component("listUsers", {
    data: function () {
        return {
            username: UserData.username,
            role: UserData.role,
            user: '',
            users: [],
            roles: ['ADMIN', 'PATIENT', 'DOCTOR'],
            genders: ['Male', 'Female', 'Other'],
            blockedAllowed: 'true',
            referralPressed: 'false',
            specialities: ['cardiology', 'neurology'],
            speciality: ''
        }
    },
    template: `
    <div style="height: 81.7%;">
    <h1>List of all patients</h1>
    <div id="filter">
    </div>    
    <div id="Div-panel" style="display: inline;">
            <div>
                <table class="myTable">
                    <thead>
                        <tr class="header">
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Gender</th>
                            <th>Username</th>
                            <th>Canceled appointments</th>
                            <th v-if="role==='ADMIN'">Block</th>
                            <th v-if="role==='DOCTOR'">Refferal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='users.username' v-for='user in users'>
                            <td>{{user.name}}</td>
                            <td>{{user.surname}}</td>
                            <td>{{user.gender}}</td>
                            <td>{{user.username}}</td>
                            <td>{{user.canceledAppointments}}</td>
                            <td>
                            <button v-if="role==='ADMIN' && !user.isBlocked&& user.canceledAppointments>2" v-on:click= "block(user.id,user.username)"  class="buttonChoose" style="background-image: url('../images/block.png');"></button>
                            <button v-if="role==='ADMIN' && user.isBlocked && user.canceledAppointments>2"  class="buttonChoose" style="background-image: url('../images/blocked.svg');" disabled></button>
                            <button v-if="role==='DOCTOR' && user.ReferralId!='0'" v-on:click= "referr(user.speciality, user.id)">send referral</button>
                            <button v-if="role==='DOCTOR'" v-on:click= "perscribe( user.id)">issue perscription</button>
                            
                            <select v-if="role==='DOCTOR'" id='listOfSpecialities' v-model="user.speciality">
                                <option disabled value="">Specialities</option>
                                <option v-for="s in specialities">{{s}}</option>
                            </select>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        </div>
    </div>
    `,

    methods: {
        getAllUsers() {
            //gets all users and puts them in users[]
            axios
                .get('getUsers')
                .then(Response => (this.users = Response.data));

            console.log(this.users);
        },
        perscribe(id) {
            localStorage.setItem("patientId", id);
            this.$router.push('/newPerscription');
        },
        referr(speciality, patientId) {
            Swal.fire({
                title: 'Are you sure you want to issue a refferal ?',
                text: "Referral will be issued",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ffdff0',
                cancelButtonColor: '#c41088',
                confirmButtonText: 'Yes, Im sure!'
            }).then((result) => {
                if (result.isConfirmed) {
                    //block user
                    axios
                        .post(`referral/create/` + speciality + '/' + patientId)
                        .then(Response => {
                            console.log("ucreated");

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'referral has been issued',
                                showConfirmButton: false,
                                timer: 3500
                            })
                            setTimeout(() => window.location.reload(), 3500);

                            this.$router.push('/patients');
                            window.location.reload();
                        })

                }
            })
        },
        block(id, username) {
            Swal.fire({
                title: 'Are you sure you want to block ' + username + ' ?',
                text: "User will be blocked",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ffdff0',
                cancelButtonColor: '#c41088',
                confirmButtonText: 'Yes, Im sure!'
            }).then((result) => {
                if (result.isConfirmed) {
                    //block user
                    axios
                        .put(`blockUser/` + id)
                        .then(Response => {
                            console.log("user blocked");

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'User' + ' has been blocked',
                                showConfirmButton: false,
                                timer: 3500
                            })
                            setTimeout(() => window.location.reload(), 3500);

                            this.$router.push('/patients');
                            window.location.reload();
                        })

                }
            })
        },
        reset() {
            if (this.user.role == "ADMIN" || this.user.role == "DOCTOR") {
                this.getAllUsers();
            }

        },

    },

    created() {
        if (UserData == {}) {
            this.$router.push('/login');

        }
        else if (UserData.role === 'PATIENT') {
            this.$router.push('/profileUser');
        }
        else {
            this.getAllUsers();
        }

    }
})