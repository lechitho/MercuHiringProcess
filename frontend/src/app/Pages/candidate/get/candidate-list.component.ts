import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { CandidateService, Candidate } from 'src/app/Services/candidate.service';
import {
  CdkDragDrop,
  CdkDrag,
  CdkDropList,
  CdkDropListGroup,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html'
})
export class CandidateListComponent implements OnInit {

  candidate!: Candidate[];
  isLoading: boolean = false;
  stage: any = ["Applied","Interviewing","Offered","Hired"];
  applied!: Candidate[];
  interviewing!: Candidate[];
  offered!: Candidate[];
  hired!: Candidate[];
  loadingTitle!: string;
  constructor(private CandidateService: CandidateService) {
  }
  ngOnInit(): void {
    this.getcandidates();
  }
  getcandidates() {
    this.loadingTitle = "loading candidate ..."; 
    this.isLoading = true;
    this.CandidateService.getcandidates().subscribe(
      (res: any) => {
        this.applied = res.filter((candidate: { stage: string; }) => candidate.stage === "Applied");
        this.interviewing = res.filter((candidate: { stage: string; }) => candidate.stage === "Interviewing");
        this.offered = res.filter((candidate: { stage: string; }) => candidate.stage === "Offered");
        this.hired = res.filter((candidate: { stage: string; }) => candidate.stage === "Hired");
        this.candidate = res;
        this.isLoading = false;
      }
    );
    
  }
  
}
