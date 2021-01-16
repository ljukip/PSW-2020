Vue.component("newAppointment", {
    data: function () {
        return {
            doctorString: '',
            doctor: {
                id: '',
                name: '',
                surname: '',
                speciality: '',
                date: '',
                time: ''
            },
            //obrisati kad proradi axios
            doctors: [],
            filteredDoctors: {
                id: '',
                name: '',
                surname: '',
                speciality: '',
                date: ''
            },
            dates: {
                from: new Date(2021, 9, 16),
                to: new Date(2021, 9, 16)
            },
            table: false,
            priorityString: '',
            priority: '',
            message: '',
            timesFrom: [{ hour: '07', min: '00', am_pm: 'AM' }, { hour: '07', min: '30', am_pm: 'AM' }, { hour: '08', min: '00', am_pm: 'AM' }, { hour: '08', min: '30', am_pm: 'AM' }, { hour: '09', min: '00', am_pm: 'AM' }],
            timesTo: [{ hour: '07', min: '30', am_pm: 'AM' }, { hour: '08', min: '00', am_pm: 'AM' }, { hour: '08', min: '30', am_pm: 'AM' }, { hour: '09', min: '00', am_pm: 'AM' }, { hour: '09', min: '30', am_pm: 'AM' }],
            timeFromString: '',
            timeFrom: { hour: '', min: '', am_pm: '' },
            timeToString: '',
            timeTo: { hour: '', min: '', am_pm: '' }
        }
    },
    template: `
    <div style="height: 81.7%;">
    <h1>Create new appointment</h1>
    <div id="filter" style="flex-direction: row;">
        <nav>
            <hr style='background:#c41088;height:4px;'>
            <label class="label1">Criteria</label>
            <form class="form-inline">
                
                <div style="flex-direction: row; display: inline-flex;">
                    <vuejsDatepicker  placeholder="From Date"  v-model="dates.from"  :highlighted="dates">
                    </vuejsDatepicker> 
                    <p>-</p>  
                    <vuejsDatepicker placeholder="To Date" v-model="dates.to"  :highlighted="dates">
                    </vuejsDatepicker>
                </div>

                <select id='timeFrom' v-model="timeFromString">
                    <option disabled value="">Starting time</option>
                    <option v-for="time in timesFrom">{{time.hour}}:{{time.min}} {{time.am_pm}}</option>
                </select>

                <select id='timeTo' v-model="timeToString">
                    <option disabled value="">Ending time</option>
                    <option v-for="time in timesTo">{{time.hour}}:{{time.min}} {{time.am_pm}}</option>
                </select>

                <input type="radio" id="doctor" value="Doctor" v-model="priorityString">
                <label for="doctor">Doctor</label>
                <input type="radio" id="dates" value="dates" v-model="priorityString">
                <label for="dates">Dates</label>

                <select id='doctor' v-model="doctorString">
                    <option disabled value="">Doctors</option>
                    <option v-for="doctor in doctors">{{doctor.id}},{{doctor.name}},{{doctor.surname}}</option>
                </select>
                
                <button class="button2" type="submit" v-on:click="search()">Search</button>
                <button style='margin-right:5px;' v-on:click="reset()" class="button2" type="button">Reset</button>
            </form>
            <hr style='background:#c41088;height:4px;'>
        </nav>
    </div>    
    <div id="Div-panel" style="display: inline;">
            <div v-if="table==='true'">
                <table class="myTable">
                    <thead>
                        <tr class="header">
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Speciality</th>
                            <th>Date and time</th>
                            <th>Appointment</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='filteredDoctors.id'>
                            <td>{{filteredDoctors.name}}</td>
                            <td>{{filteredDoctors.surname}}</td>
                            <td>{{filteredDoctors.speciality}}</td>
                            <td>{{filteredDoctors.dateTimeFrom.toString()}}</td>
                            <td><button class="buttonChoose" v-on:click= "newAppointment()" type="button"></button></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        </div>
    </div>

    `,
    methods: {
        newAppointment() {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Appointment has been created',
                showConfirmButton: false,
                timer: 1400
            })
            this.$router.push('/appointments');
        },
        search() {
            if (this.dates.from == '') {
                this.message = 'wrongDate';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.dates.to == '') {
                this.message = 'wrongDate';
                setTimeout(() => this.messageVal = '', 6000);
            }
            /*else if (this.doctor == '') {
                this.message = 'wrongSpecialist';
                setTimeout(() => this.messageVal = '', 6000);
            }
            else if (this.priority == '') {
                this.message = 'wrongPriority';
                setTimeout(() => this.messageVal = '', 6000);
            }*/
            else {
                let am_pm = this.timeFromString.split(" ");
                this.timeFrom.am_pm = am_pm[1];
                let time = am_pm[0].split(":");
                this.timeFrom.hour = time[0];
                this.timeFrom.min = time[1];
                am_pm = this.timeToString.split(" ");
                this.timeTo.am_pm = am_pm[1];
                time = am_pm[0].split(":");
                this.timeTo.hour = time[0];
                this.timeTo.min = time[1];
                let id = this.doctorString.split(",");
                this.doctor.id = id[0];

                this.dates.from.setHours(this.timeFrom.hour);
                this.dates.from.setMinutes(this.timeFrom.min);
                this.dates.to.setHours(this.timeTo.hour);
                this.dates.to.setMinutes(this.timeTo.min);
                this.priority = this.priorityString;

                console.log(this.dates.to + this.dates.from.toISOString() + this.doctor.id + this.priorityString + this.priority);

                axios
                    .get('getSuggestions/' + this.dates.from.toISOString() + '/' + this.dates.to.toISOString() + '/' + this.priority + '/' + this.doctor.id)
                    .then((responce) => this.suggestions(responce.data))
                    .catch(() => this.failed());
            }


        },
        suggestions(data) {
            this.filteredDoctors = data;
            this.table = 'true';

        },
        reset() {
            this.priority = '';
            this.doctor = '';
            this.dates.from = '';
            this.dates.to = '';
            this.table = 'false';
        },
    },
    components: {
        vuejsDatepicker
    },
    created() {
        axios
            .get('allDoctors')
            .then(Response => (this.doctors = Response.data));

    },
})