export default {
    methods: {
        formatDate(d, format) {
            const date = new Date(d);
            let month = (date.getMonth() + 1).toString();
            let day = date.getUTCDate().toString();
            const year = date.getFullYear().toString();
            if (month.length < 2) {
                month = "0" + month;
            }
            if (day.length < 2) {
                day = "0" + day;
            }

            switch (format) {
                case "yyyy-mm-dd":
                    return [year, month, day].join("-");

                case "mm/dd/yyyy":
                    return [month, day, year].join("/");

                case "mm-dd-yyyy":
                    return [month, day, year].join("-");

                default:
                    return [year, month, day].join("-");
            }
        }
    }
}