import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { CandidateService, Candidate } from '../../../Services/candidate.service';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.css']
})
export class CandidateListComponent implements OnInit{

  candidate!: Candidate[];
  isLoading: boolean = false;
  stage: any = ["Applied","Interviewing","Offered","Hired"];
  loadingTitle!: string;
  currentItem!: Candidate;

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
        this.candidate = res;
        this.isLoading = false;
      }
    );
  }
  
  filterCandidates(stage: string) {
    return this.candidate.filter((candidate) => candidate.stage === stage);
  }

  onDragStart(item: any) {
    console.log('onDragStart');
    this.currentItem = item;
  }

  onDrop(event: any, stage: string) {
    event.preventDefault();
    console.log(this.currentItem);
    if (this.currentItem) {
      this.currentItem.stage = stage;
      this.CandidateService.updatecandidate(this.currentItem, this.currentItem.id).subscribe(
        (res: any) => {
          this.getcandidates();
        }
      );
    }
    
  }

  onDragOver(event: any) {
    event.preventDefault();
  }
}
