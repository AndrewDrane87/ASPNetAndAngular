import { EventEmitter, Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { InformationModalComponent } from '../play/modals/information-modal/information-modal.component';
import { ChallengeModalComponent } from '../play/modals/challenge-modal/challenge-modal.component';
import { ActionTrigger } from '../_models/actionTrigger';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TriggerService {
  bsModalRef: BsModalRef | undefined;
  modalConfig = {
    backdrop: false,
    ignoreBackdropClick: true,
  };
  baseUrl = environment.apiUrl;

  triggerArray: ActionTrigger[] = [];
  complete = new EventEmitter();
  constructor(
    private http: HttpClient,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) {}

  checkTriggers(triggers: ActionTrigger[], eventType: string) {
    this.triggerArray = triggers;
    this.processTrigger(0, eventType);
  }

  processTrigger(currentIndex: number, eventType: string) {
    // console.log(`Checking Triggers - currentIndex: ${currentIndex}`);
    if (currentIndex >= this.triggerArray.length) {
      this.complete.emit();
      return;
    }
    
    let t = this.triggerArray[currentIndex];
    // console.log(`t.eventType=${t!.eventType}; eventType=${eventType}`);

    if (t!.eventType === eventType) {
      switch (t!.actionType) {
        case 'information':
          this.showInformationModal(t!.actionData)!.subscribe({
            next: () => {
              this.http.put(this.baseUrl + `adventures/update-trigger-save?triggerSaveId=${t.id}&isComplete=true&result='na'`,{}).subscribe({next:()=>{
                this.processTrigger(currentIndex + 1, eventType);
              }});
            },
          });
          break;

        case 'challenge':
          this.showChallengeModal(t!.actionData)!.subscribe({
            next: () => {
              this.processResults(
                t!.resultData,
                this.bsModalRef!.content.result
              ).subscribe({
                next: (challengeResult) => {
                  this.http.put(this.baseUrl + `adventures/update-trigger-save?triggerSaveId=${t.id}&isComplete=true&result=${challengeResult}`,{}).subscribe({next:()=>{
                    this.processTrigger(currentIndex + 1, eventType);
                  }});
                },
              });
            },
          });
          break;

        default:
          this.processTrigger(currentIndex + 1, eventType);
          break;
      }

    } else {
      this.processTrigger(currentIndex + 1, eventType);
    }
  }

  showInformationModal(information: string) {
    this.bsModalRef = this.modalService.show(InformationModalComponent);
    this.bsModalRef.content.information = information;
    return this.bsModalRef.onHidden;
  }

  showChallengeModal(actionData: string) {
    this.bsModalRef = this.modalService.show(
      ChallengeModalComponent,
      this.modalConfig
    );
    this.bsModalRef.content.setActionData(actionData);
    return this.bsModalRef.onHidden;
  }

  processResults(resultData: string, resultValue: boolean): Observable<any> {
    return new Observable((observer) => {
      resultValue
        ? this.toastr.success('Check Passed')
        : this.toastr.error('Check Failed');
      observer.next(resultValue ? 'Success' : 'Failure');
    });
  }
}
