Vue.component("listUsers", {
    data: function () {
        return {
            user: {
                username: localStorage.getItem('username'),
                role: localStorage.getItem('role')
            },
            users: [],
            searchedUser: {
                name: '',
                surname: '',
                username: '',
                gender: null,
                canceledAppointments: null,
            },
            roles: ['ADMIN', 'PATIENT'],
            genders: ['Male', 'Female', 'Other'],
            searchedQuery: '?',
            blockedAllowed: 'false',
            referralPressed: 'true',
            speciality: ['cardiology', 'neurology']
        }
    },
    template: `
    <div style="height: 81.7%;">
    <h1>List of all patients</h1>
    <div id="filter">
        <nav>
            <hr style='background:#c41088;height:4px;'>
            <label class="label1">Search</label>
            <form class="form-inline">
                <input style="width:14% ;" v-model='searchedUser.name' type="text"
                    placeholder="name" aria-label="Search">
                <input style="width:14% ;" v-model='searchedUser.surname' type="text"
                    placeholder="surname" aria-label="Search">
                <input style="width:14% ;" v-model='searchedUser.username' type="text"
                    placeholder="username" aria-label="Search">
                <select id='listOfGenders'
                    v-model="searchedUser.gender">
                    <option disabled value="">Gender</option>
                    <option v-for="gender in genders">{{gender}}</option>
                </select>
                <button class="button2" type="submit" v-on:click="search()">Search</button>
                <button style='margin-right:5px;' v-on:click="reset()" class="button2" type="button">Reset</button>
            </form>
            <hr style='background:#c41088;height:4px;'>
        </nav>
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
                            <th v-if="user.role==='ADMIN'">Block</th>
                            <th v-if="user.role==='DOCTOR'">Refferal</th>
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
                            <button v-if="user.role==='ADMIN' && blockedAllowed==='true'" class="buttonDelete"></button>
                            <button v-if="user.role==='DOCTOR'">referral</button>
                            <select v-if="referalPressed==='true'" id='listOfSpecialities' v-model="speciality">
                                <option disabled value="">Speciality</option>
                                <option v-for="s in speciality">{{s}}</option>
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
                .get('users/all/' + this.user.role)
                .then(Response => (this.users = Response.data));
        },
        search() {
            if (this.searchedUser.name !== '') {
                this.searchedQuery += 'name=' + this.searchedUser.name;
            }
            if (this.searchedUser.surname !== '') {
                this.searchedQuery += '&surname=' + this.searchedUser.surname;
            }
            if (this.searchedUser.username !== '') {
                this.searchedQuery += '&username=' + this.searchedUser.username;
            }
            if (this.searchedUser.gender !== null) {
                this.searchedQuery += '&gender=' + this.searchedUser.gender;
            }
            if (this.searchedUser.role !== null) {
                this.searchedQuery += '&role=' + this.searchedUser.role;
            }

            axios
                .get('user/search/' + this.user.role + this.searchedQuery)
                .then(response => {
                    this.users = response.data;
                    this.searchedQuery = '?';
                });

        },
        reset() {
            if (this.user.role == "ADMIN") {
                this.getAllUsers();
            }

            this.searchedQuery = '?';

        },

    },

    created() {
        this.role = localStorage.getItem('role');
        this.getAllUsers();
    }
})