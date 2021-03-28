import { CanDeactivate } from "@angular/router";
import { CompanyEditComponent } from "../companyEdit/companyEdit.component";


export class PreventUnsavedChanges implements CanDeactivate<CompanyEditComponent>{
    canDeactivate(component: CompanyEditComponent){
        if(component.editForm.dirty){
            return confirm('Czy jesteś pewien, że chcesz opuścić to okno? Wszytskie nie zapisane zmiany zostaną utracone!');
        }
        return true;
    }
}