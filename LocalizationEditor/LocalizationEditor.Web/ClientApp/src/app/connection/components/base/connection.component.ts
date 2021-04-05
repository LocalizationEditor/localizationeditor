import {Component, Input} from "@angular/core";
import {IConnection} from "../../models/Connection/IConnection";

@Component({
  templateUrl: 'connection.component.html',
  selector: 'connection-model'
})
export class ConnectionComponent {
  @Input() className: string;
  @Input() connection: IConnection;
}
