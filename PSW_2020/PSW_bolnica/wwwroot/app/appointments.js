Vue.component("appointments", {
    data: function () {
        return {
            appointmentsPassed: [],
            appointmentsFuture: [],
            appointments: [],
        }
    },
    template: `
    <div style="height: 81.7%;">
    <nav style="background-color: lavenderblush;">
        <hr style='background:#c41088;height:4px;'>
        <label class="label1">Future appointments</label>
        <hr style='background:#c41088;height:4px;'>
    </nav>
    <div id="Div-panel" style="display: inline;">
            <div>
                <table class="myTable">
                    <thead>
                        <tr class="header">
                            <th>Doctor</th>
                            <th>Speciality</th>
                            <th>Date and time</th>
                            <th>Cancel</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='appointment.id' v-for='appointment in appointmentsFuture'>
                            <td>{{appointment.doctor.name}}, {{appointment.doctor.surname}}</td>
                            <td>{{appointment.doctor.speciality}}</td>
                            <td>{{appointment.dateTimeFrom}}</td>
                            <td><button class="buttonChoose" style="background-image: url('../images/cnc.png');" v-on:click= "cancel(appointment.id, appointment.dateTimeFrom)" type="button"></button></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <nav style="background-color: lavenderblush;">
            <hr style='background:#c41088;height:4px;'>
            <label class="label1">Passed appointments</label>
            <hr style='background:#c41088;height:4px;'>
        </nav>
        <div id="Div-panel" style="display: inline;">
                <div>
                    <table class="myTable">
                        <thead>
                            <tr class="header">
                                <th>Doctor</th>
                                <th>Speciality</th>
                                <th>Date and time</th>
                                <th>Review</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-bind:key='appointment.id' v-for='appointment in appointmentsPassed'>
                                <td>{{appointment.doctor.name}}, {{appointment.doctor.surname}}</td>
                                <td>{{appointment.doctor.speciality}}</td>
                                <td>{{appointment.dateTimeFrom}}</td>
                                <td><button class="buttonChoose" style="background-image: url('../images/review.png');" v-on:click= "review()" type="button"></button></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
    `,
    methods: {
        cancel(id, date) {
            var today = new Date();
            today.setDate(today.getDate() + 2);
            if (today.toISOString() > date) {
                Swal.fire({
                    icon: 'error',
                    title: 'Cancelation time passed',
                    text: 'You have less then 48h to the appointment!',
                })
            }
            else {
                Swal.fire({
                    title: 'Are you sure you want to cancel the appointment?',
                    text: "Appointment will be canceled",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#ffdff0',
                    cancelButtonColor: '#c41088',
                    confirmButtonText: 'Yes, Im sure!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        axios
                            .put(`cancel/` + id)
                            .then(Response => {

                                Swal.fire({
                                    position: 'top-end',
                                    icon: 'success',
                                    title: 'Appointment has been canceled!',
                                    showConfirmButton: false,
                                    timer: 3500
                                })
                                setTimeout(() => window.location.reload(), 3500);
                            })
                    }
                })
            }


        },
        review() {
            this.$router.push('/review');
            //axios  + appointment.id

        },
        load(data) {
            this.appointments = data;
            let date = new Date();
            for (let i = 0; i < this.appointments.length; i++) {
                console.log(this.appointments[i].dateTimeFrom + date.toISOString());
                if (this.appointments[i].dateTimeFrom < date.toISOString()) {
                    this.appointmentsPassed.push(this.appointments[i]);
                }
                else {
                    this.appointmentsFuture.push(this.appointments[i]);
                }

            }
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
        else {
            axios
                .get('patientAppointments/' + UserData.username)
                .then(Response => this.load(Response.data));
        }

    }
})