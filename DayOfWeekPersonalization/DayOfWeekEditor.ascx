<label for="day" class="sfTxtLbl">{$CustomPersonalizationResources, Day$}</label>

<select id="day">
    <option value="1">{$CustomPersonalizationResources, Monday$}</option>
    <option value="2">{$CustomPersonalizationResources, Tuesday$}</option>
    <option value="3">{$CustomPersonalizationResources, Wednesday$}</option>
    <option value="4">{$CustomPersonalizationResources, Thursday$}</option>
    <option value="5">{$CustomPersonalizationResources, Friday$}</option>
    <option value="6">{$CustomPersonalizationResources, Saturday$}</option>
    <option value="0">{$CustomPersonalizationResources, Sunday$}</option>
</select>

<script type="text/javascript">

    function CriterionEditor() {
    }

    CriterionEditor.prototype = {
        //Used as a label for the criterion when viewing the user segment
        getCriterionTitle: function () {
            return "{$CustomPersonalizationResources, Day$}";
        },
        //The descriptive value for the criterion
        getCriterionDisplayValue: function () {
            return $("#day option:selected").text();

        },

        //Persists the input from the user as a value for the criterion
        getCriterionValue: function () {
            return $("#day").val();
        },

        //When the editor opens, sets the previously persisted value
        //as initial value for the criterion
        setCriterionValue: function (val) {
            $("#day").val(val);
        },

        errorMessage: "",

        isValid: function () {
            var day = $("#day").val();
            if (day.length === 0) {
                this.errorMessage = "{$CustomPersonalizationResources, DayError$}";
                return false;
            }

            return true;
        }
    };
</script>