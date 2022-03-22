import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, forwardRef, Input, OnInit } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { VirtualScrollComponent } from '../virtual-scroll/virtual-scroll.component';

class ChecklistNode {
  id: number = 0;
  parentSectionId: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  checked: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  childrenPartiallyChecked: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  text: string = '';
  childrenSubject: BehaviorSubject<ChecklistNode[]> = new BehaviorSubject<
    ChecklistNode[]
  >([]);

  constructor(text: string, id: number) {
    this.text = text;
    this.id = id;
  }

  addChild(newChild: ChecklistNode) {
    this.childrenSubject.value.push(newChild);
    this.childrenSubject.next([...this.childrenSubject.value]);
  }

  removeChild(childId: number) {
    const newChildren: ChecklistNode[] = this.childrenSubject.value.filter(
      (child) => child.id !== childId
    );
    this.childrenSubject.next(newChildren);
  }
}

export interface ChecklistItem {
  content: string;
  checked: boolean;
}

export interface ChecklistSection {
  sectionName: string;
  checklist: ChecklistItem[];
}

@Component({
  selector: 'app-form-checklist-with-sections',
  templateUrl: './form-checklist-with-sections.component.html',
  styleUrls: ['./form-checklist-with-sections.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormChecklistWithSectionsComponent),
      multi: true,
    },
  ],
})
export class FormChecklistWithSectionsComponent
  extends VirtualScrollComponent
  implements OnInit, ControlValueAccessor
{
  @Input() labelPosition: 'before' | 'after' = 'after';
  @Input() showCheckboxes: boolean = true;
  @Input() title: string = 'Checklist';
  nestedTreeControl: NestedTreeControl<ChecklistNode>;
  nestedDataSource: MatTreeNestedDataSource<ChecklistNode>;
  dataChange: BehaviorSubject<ChecklistNode[]> = new BehaviorSubject<
    ChecklistNode[]
  >([]);
  containerParent!: ChecklistNode;
  private nextNodeId: number = 2;
  @Input() formControlForManipulation!: FormControl;
  constructor() {
    super();
    this.nestedDataSource = new MatTreeNestedDataSource<ChecklistNode>();
    this.dataChange.subscribe(
      (data: ChecklistNode[]) => (this.nestedDataSource.data = data)
    );
    this.nestedTreeControl = new NestedTreeControl<ChecklistNode>(
      this.getChildren
    );
  }

  writeValue(obj: ChecklistSection[]): void {
    obj.forEach((checklistSection) => {
      const parent = new ChecklistNode(
        checklistSection.sectionName,
        this.nextNodeId++
      );
      checklistSection.checklist.forEach((checklistItem) => {
        const child = new ChecklistNode(
          checklistItem.content,
          this.nextNodeId++
        );
        child.checked.next(checklistItem.checked);
        child.parentSectionId.next(parent.id);
        parent.addChild(child);
      });
      parent.childrenPartiallyChecked.next(this.someChildrenChecked(parent));
      parent.checked.next(this.allChildrenChecked(parent));
      parent.parentSectionId.next(1);
      this.containerParent.addChild(parent);
    });
    this.containerParent.childrenPartiallyChecked.next(
      this.someChildrenChecked(this.containerParent)
    );
    this.containerParent.checked.next(
      this.allChildrenChecked(this.containerParent)
    );
    this.dataChange.next([...this.dataChange.value]);
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onTouched = () => {};

  onChange = (value: any) => {};

  ngOnInit(): void {
    this.nextNodeId = 2;
    this.containerParent = new ChecklistNode(this.title, 1);
    this.containerParent.checked.next(true);
    this.dataChange.next([this.containerParent]);
  }

  private getChildren(
    checklistNode: ChecklistNode
  ): BehaviorSubject<ChecklistNode[]> {
    return checklistNode.childrenSubject;
  }

  hasNestedChild(_: number, node: ChecklistNode): boolean {
    return (
      node.childrenSubject.value.length > 0 ||
      node.id === 1 ||
      node.parentSectionId.value === 1
    );
  }

  isInputNode(_: number, node: ChecklistNode): boolean {
    return node.id === -1;
  }

  hasInputChild(node: ChecklistNode): boolean {
    return (
      node.childrenSubject.value.length > 0 &&
      node.childrenSubject.value[node.childrenSubject.value.length - 1].id ===
        -1
    );
  }

  leafNodeChecked(
    checkboxChecked: boolean,
    checklistNode: ChecklistNode
  ): void {
    checklistNode.checked.next(checkboxChecked);
    this.updateParents(
      this.dataChange.value[0],
      checklistNode.parentSectionId.value
    );
    this.onTouched();
    this.onChange(this.nodesToChecklistSections());
  }

  allChildrenChecked(checklistNode: ChecklistNode): boolean {
    return checklistNode.childrenSubject.value.every(
      (childChecklistNode: ChecklistNode) => childChecklistNode.checked.value
    );
  }

  someChildrenChecked(checklistNode: ChecklistNode): boolean {
    return (
      checklistNode.childrenSubject.value.some(
        (childChecklistNode: ChecklistNode) => childChecklistNode.checked.value
      ) && !this.allChildrenChecked(checklistNode)
    );
  }

  updateAllChildrenAndParentIfExists(
    checkboxChecked: boolean,
    checkboxListNode: ChecklistNode
  ): void {
    this.updateAllChildren(checkboxChecked, checkboxListNode);
    if (checkboxListNode.parentSectionId.value !== 0)
      this.updateParents(
        this.dataChange.value[0],
        checkboxListNode.parentSectionId.value
      );
  }

  updateAllChildren(
    checkboxChecked: boolean,
    checklistNode: ChecklistNode
  ): void {
    checklistNode.checked.next(checkboxChecked);
    checklistNode.childrenSubject.value.forEach(
      (childChecklistNode: ChecklistNode) =>
        this.updateAllChildren(checkboxChecked, childChecklistNode)
    );
  }

  updateParents(checklistNode: ChecklistNode, parentId: number): boolean {
    if (parentId === checklistNode.id) {
      checklistNode.checked.next(this.allChildrenChecked(checklistNode));
      checklistNode.childrenPartiallyChecked.next(
        this.someChildrenChecked(checklistNode)
      );
      if (checklistNode.parentSectionId.value !== 0)
        this.updateParents(
          this.dataChange.value[0],
          checklistNode.parentSectionId.value
        );
      return true;
    }
    return checklistNode.childrenSubject.value.some((children) => {
      this.updateParents(children, parentId);
    });
  }

  removeChecklistNodeHandler(checklistNodeToDelete: ChecklistNode): void {
    this.removeChecklistNode(this.dataChange.value[0], checklistNodeToDelete);
  }

  removeChecklistNode(
    parentChecklistNode: ChecklistNode,
    checklistNodeToDelete: ChecklistNode
  ): boolean {
    if (
      parentChecklistNode.id === checklistNodeToDelete.parentSectionId.value
    ) {
      parentChecklistNode?.removeChild(checklistNodeToDelete.id);
      this.updateParents(
        this.dataChange.value[0],
        checklistNodeToDelete.parentSectionId.value
      );
      return true;
    }
    return parentChecklistNode.childrenSubject.value.some((child) => {
      this.removeChecklistNode(child, checklistNodeToDelete);
    });
  }

  insertInputChecklistNode(parentChecklistNode: ChecklistNode): void {
    if (!this.hasInputChild(parentChecklistNode)) {
      const inputNode = new ChecklistNode(
        parentChecklistNode.text + ' item',
        -1
      );
      inputNode.parentSectionId.next(parentChecklistNode.id);
      inputNode.childrenSubject = parentChecklistNode.childrenSubject;
      parentChecklistNode.addChild(inputNode);
      this.nestedTreeControl.expand(parentChecklistNode);
    }
  }

  insertChecklistNode(
    inputChecklistNode: ChecklistNode,
    checklistNodeText: string
  ) {
    const checklistNode = new ChecklistNode(
      checklistNodeText,
      this.nextNodeId++
    );
    checklistNode.parentSectionId.next(
      inputChecklistNode.parentSectionId.value
    );
    inputChecklistNode.addChild(checklistNode);
    this.removeChecklistNodeHandler(inputChecklistNode);
    this.onTouched();
    this.onChange(this.nodesToChecklistSections());
  }

  nodeToChecklistSection(node: ChecklistNode) {
    let checklist: ChecklistSection = { sectionName: node.text, checklist: [] };
    if (node.childrenSubject.value.length > 0) {
      node.childrenSubject.value.forEach((child) =>
        checklist.checklist.push({
          content: child.text,
          checked: child.checked.value,
        })
      );
    }
    return checklist;
  }

  nodesToChecklistSections() {
    const checklistSections: ChecklistSection[] = [];
    this.dataChange.value[0].childrenSubject.value.forEach((child) =>
      checklistSections.push(this.nodeToChecklistSection(child))
    );
    return checklistSections;
  }
}
