import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../service/vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes : any[];
  features : any[];
  models : any[];
  
  vehicle : any= {
    features:[],
    contact : {}
  };
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMake().subscribe(m => {
      this.makes = m;
    });

    this.vehicleService.getFeatures().subscribe(f =>{
      this.features = f;
    });
  }

  onMakeChange(){
    var selectedMake=this.makes.find(m => m.id == this.vehicle.makeId);
    this.models= selectedMake? selectedMake.models:[];
    delete this.vehicle.modelId;
  }
  onFeatureToggle(id:any, $event:any){
    if($event.target.checked)
      this.vehicle.features.push(id);
    else{
        var index = this.vehicle.features.indexOf(id);
        this.vehicle.features.splice(index,1);
      }
  }
}
