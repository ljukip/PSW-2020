Vue.component("registration", {
    data: function () {
        return {
            user: {
                name: '',
                surname: '',
                gender: '',
                username: '',
                password1: '',
                password2: '',
                role: 'PATIENT', //samo pacijent moze da se registruje
                address: '',
                phoneNumber: ''

            },
            messageVal: ''
        }

    },
    template: `
    <div>
        <center> <h1 style="color:rgb(82, 7, 63);font-family: cursive;"> registration<br></h1> </center> 
        <div id="div-fit">
            <form @submit.prevent="" class="form-space">
                <div class="container"> 
                <br> 
                <div>
                <div v-if="messageVal==='wrongName'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Wrong name format</div>
                    <label class="label1">Name : </label> 
                </div>
                <div id="center">
                    <input v-model="user.name" type="text" placeholder="Enter Name" name="name" required>
                </div>
                <div>
                    <div v-if="messageVal==='wrongSurname'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Wrong surname format</div>
                    <label class="label1">Surname : </label> 
                </div>
                <div id="center">
                    <input v-model="user.surname" type="text" placeholder="Enter surname" name="surname" required>
                </div>
                <div>
                    <div v-if="messageVal==='wrongGender'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Please select gender</div>
                    <label class="label1">Gender : </label> 
                </div>
                <div id="center">
                    <input type="radio" id="male" name="gender" value="male" v-model="user.gender" required>
                    <label for="male" class="label2">Male</label>
                    <input type="radio" id="female" name="gender" value="female" v-model="user.gender" required>
                    <label for="female" class="label2">Female</label>
                    <input type="radio" id="other" name="gender" value="other" v-model="user.gender" required>
                    <label for="other" class="label2">Other</label>
                </div>
                <br>

                <div>
                    <div v-if="messageVal==='wrongAddress'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Wrong address format</div>
                    <label class="label1">Address : </label> 
                </div>
                <div id="center">
                    <input v-model="user.address" type="text" placeholder="Enter address" name="address" required>
                </div>
                <div>
                    <div v-if="messageVal==='wrongPhoneNumber'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Wrong phone number format</div>
                    <label class="label1">Phone number : </label> 
                </div>
                <div id="center">
                    <input v-model="user.phoneNumber" type="text" placeholder="Enter phone number" name="phoneNumber" required>
                </div>

                <div>
                    <div v-if="messageVal==='wrongUsername'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Wrong username format</div>
                    <div v-if="messageVal==='UserExists'" style="color:  #c41055;text-align: center;font-family: cursive;font-size: 21;">Username already exists</div>
                    <label class="label1">Username : </label> 
                </div>
                <div id="center">
                    <input v-model="user.username" type="text" placeholder="Enter Username" name="username" required>
                </div>
                <div>
                    <div v-if="messageVal==='wrongPassword'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Please enter a password</div>
                    <label class="label1">Password : </label> 
                </div>
                <div id="center">
                    <input v-model="user.password1" type="password" placeholder="Enter Password" name="password1" required>
                </div>
                <div>
                    <div v-if="messageVal==='wrongConfirm'" style="color:  #c41088;text-align: center;font-family: cursive;font-size: 21;">Please confirm password</div>
                    <label class="label1">Repeat password : </label> 
                </div>
                <div id="center">
                    <input v-model="user.password2" type="password" placeholder="Repeat Password" name="password2" required>
                </div>
                <div v-if="messageVal==='wrongMatch'" style="color:  #c41055;text-align: center;font-family: cursive;font-size: 21;">Passwords do not match</div>
                <div id="center">
                <button class="button1" type="submit" v-on:click='registration(user)'>Register</button> 
                <button class="button1" type="button" v-on:click='cancel()' > Cancel</button> 
                </div>
                </div>
            </form>  
        </div> 
    </div>
    
    `,
    methods: {
        cancel: function () {
            Swal.fire({
                title: 'Are you sure you want to cancel changes?',
                text: "Registration will be canceled",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ffdff0',
                cancelButtonColor: '#c41088',
                confirmButtonText: 'Yes, Im sure!'
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$router.push('/');
                    window.location.reload();
                }
            })
        },

        registration: function (user) {
            if (this.user.name == '') {
                console.log("wrong name");
                this.messageVal = 'wrongName';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.surname == '') {
                this.messageVal = 'wrongSurname';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.gender == '') {
                this.messageVal = 'wrongGender';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.username == '') {
                this.messageVal = 'wrongUsername';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.address == '') {
                this.messageVal = 'wrongAddress';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.phoneNumber == '') {
                this.messageVal = 'wrongPhoneNumber';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.password1 == '') {
                this.messageVal = 'wrongPassword';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.user.password2 == '') {
                this.messageVal = 'wrongConfirm';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else {
                if (this.user.password1 !== this.user.password2) {
                    console.log("not a match");
                    this.messageVal = 'wrongMatch';
                    setTimeout(() => this.messageVal = '', 6000);
                }
                else {
                    console.log("in axios: " + user);
                    //var sha512 = require('js-sha512');

                    axios
                        .post('registration', {
                            name: user.name,
                            surname: user.surname,
                            gender: user.gender,
                            username: user.username,
                            //password: sha512(user.password1),
                            password: user.password1,
                            role: user.role,
                            address: user.address,
                            phoneNumber: user.phoneNumber
                        })
                        .then((responce) => this.succes(responce.data))
                        .catch(() => this.failed());

                }
            }
        },

        succes: function (data) {
            console.log('prosao');
            console.log("succes");

            deleteCookie("JWT");
            setCookie("JWT", data, 86400);
            UpdateUserDataFromJWT();


            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Your work has been saved',
                showConfirmButton: false,
                timer: 1400
            })
            setTimeout(this.$router.push('/homeUser'), 2100);
            setTimeout(window.location.reload(), 2100); //load pushed
        },
        failed: function () {
            console.log('nije prosao');
            this.messageVal = 'UserExists';
            setTimeout(() => this.messageVal = '', 6000);
        }
    }
})