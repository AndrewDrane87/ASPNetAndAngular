import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'jsonConvert'
})
export class JsonConvertPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    let string = '';
    try {
      var json = JSON.parse(value);
      Object.keys(json).forEach((key) => {
        console.log(`${key}: ${json[key]}`);
        const value = json[key];
        if(value !== 0){
          string += `${this.toTitleCase(key)}: ${value} `;
        }
      });
    } catch {
      return;
    }
    
    return string;
  }

  private toTitleCase(word:string):string {
    return word.substring(0,1).toUpperCase() + 
           word.substring(1).toLowerCase();
}
}
