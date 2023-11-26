using AdminDesk.Entities;

public interface ICheckListRepository
{
    void Upsert(CheckList checklist);
    CheckList Get(int CheckListId);
    List<CheckList> GetAll();
    CheckList GetByServiceOrderId(int serviceOrderId); // Add this method

}