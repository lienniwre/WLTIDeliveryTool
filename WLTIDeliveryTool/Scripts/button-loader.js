 class ButtonLoader {
     constructor(id) {
         this.id = id;
        
     }
     
    Processing() {
        $('#' + this.id).html("<i class='fas fa-spinner fa-spin'></i> &nbsp; Processing");
    }
     Completed() {
         let x = this;
         setTimeout(function () { $('#' + x.id).html("<i class='fas fa-check-circle'></i> &nbsp; Success"); }, 500);
        $('#' + this.id).prop('disabled', true);
     }

     Default() {
         let x = this;
         setTimeout(function () { $('#' + x.id).html("<i class='fa fa-save'></i> &nbsp; Save"); }, 1000);
         $('#' + this.id).prop('disabled', false);
     }

};