
interface ILevel {
	bool ObjectivesComplete();
    void SetLevelResults();
    void GuardKilled();
    void GotWeapon();
    void ButtonPressed(string buttonName);
    void OpenPasswordEntry(string[] passwords);
}
