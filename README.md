# Sylvaar
interactive programming final exam

### For some unity settings
using git bash can skip error for folder name with space
1. setting git config merge tool
    ```bash
    git config merge.tool unityyamlmerge
    ```
2. setting mergetool trustExitCode
    ```bash
    git config  mergetool.unityyamlmerge.trustExitCode false
    ```
3. setting mergetool cmd
    ```bash
    git config  mergetool.unityyamlmerge.cmd ' "path to UnityYAMLMerge" merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED" '
    ```
   - for this project, unity version is `2022.3.47f1`
   example: 
        ```bash
        git config  mergetool.unityyamlmerge.cmd ' "D:\Unity\Unity Editor\2022.3.47f1\Editor\Data\Tools\UnityYAMLMerge.exe" merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED" '
        ```
4. disabled mergetool generated `.orig` files
    ```bash
    git config --global mergetool.keepBackup false
    ```

### How to use mergetool
when conflict, type on terminal
```bash
git mergetool
```
[reference for unity mergetool vedio tutorial](https://www.youtube.com/watch?v=ZI7Uz-VnO8U)
[reference for unity documentation](https://docs.unity3d.com/6000.0/Documentation/Manual/SmartMerge.html)