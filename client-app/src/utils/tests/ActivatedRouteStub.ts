import { convertToParamMap } from '@angular/router';
import { map, of } from 'rxjs';

export class ActivatedRouteStub {
  // Observable that contains a test param
  param = of({ id: 'test' });

  // Convert the param into paramMap
  paramMap = this.param.pipe(map(param => convertToParamMap(param)));

  // Add other methods and properties as needed
}