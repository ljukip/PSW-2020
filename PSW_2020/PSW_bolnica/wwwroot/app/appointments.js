Vue.component("appointments", {
    data: function () {
        return {
            appointments: [{ id: '', doctor: '', speciality: '', date: '', time: '' }],

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
                            <th>Date</th>
                            <th>Time</th>
                            <th>Cancel</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='appointment.id' v-for='appointment in appointments'>
                            <td>{{appointment.doctor}}</td>
                            <td>{{appointment.Speciality}}</td>
                            <td>{{appointment.date}}</td>
                            <td>{{appointment.time}}</td>
                            <td><button class="buttonChoose" style="background-image: url('../images/cnc.png');" v-on:click= "cancel()" type="button"></button></td>
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
                                <th>Date</th>
                                <th>Time</th>
                                <th>Review</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-bind:key='appointment.id' v-for='appointment in appointments'>
                                <td>{{appointment.doctor}}</td>
                                <td>{{appointment.speciality}}</td>
                                <td>{{appointment.date}}</td>
                                <td>{{appointment.time}}</td>
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
        cancel() {
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
                    //cancel appointment
                    this.$router.push('/appointments');
                    window.location.reload();
                }
            })
        },
        review() {
            this.$router.push('/review');
            //axios  + appointment.id

        },

    },
})