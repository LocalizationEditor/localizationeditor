import {Component, EventEmitter, Input, Output} from "@angular/core";

@Component({
  selector: "input-field",
  templateUrl: "input-field.html"
})
export class InputField {
  @Input() lable: string;
  @Input() type: string;
  @Input() placeholder: string;
  @Input() tooltipMessage: string;
  @Input() isRequired: boolean = false;

  @Input() value: string;
  @Output() onChange = new EventEmitter();

  changeValue(item: string) {
    this.value = item;
    this.onChange.emit(item);
  }
}
