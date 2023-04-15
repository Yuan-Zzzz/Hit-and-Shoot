using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{

    private VisualElement rightPane;
    private ListView leftPane;

    private const float brickSize = 50;
    private const float levelHeight = 10;
    private const float levelWidth = 17;

    LevelData_SO selectedLevel;

    [MenuItem("Tools/LevelEditor")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("关卡编辑器");

    }
    public void CreateGUI()
    {
      
        //加载关卡数据
        var allObjectGuids = AssetDatabase.FindAssets("t:LevelData_SO");
        var allObjects = new List<LevelData_SO>();
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<LevelData_SO>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        //创建窗口
        var splitView1 = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Horizontal);
        //  var splitView2 = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Vertical);
        rootVisualElement.Add(splitView1);

        leftPane = new ListView();
        splitView1.Add(leftPane);

        rightPane = new VisualElement();
        splitView1.Add(rightPane);


        // Initialize the list view with all sprites' names
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        leftPane.itemsSource = allObjects;
        leftPane.onSelectionChange += OnLevelSelectionChange;
    }
    private void OnLevelSelectionChange(IEnumerable<object> _selectedLevels)
    {
        
        //清除右侧内容
        rightPane.Clear();

        // Get the selected sprite
         selectedLevel = _selectedLevels.First() as LevelData_SO;
        if (selectedLevel == null) return;

        //绘制容器
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelHeight; j++)
            {
                Button holderButton = new Button(OnHolderClicked);
                holderButton.style.position = Position.Absolute;
                holderButton.text = "Holder";
                holderButton.style.height =brickSize;
                holderButton.style.width = brickSize;
                holderButton.style.left = i * brickSize;
                holderButton.style.top = j * brickSize;
                rightPane.Add(holderButton);

                //绘制砖块
                for (int k = 0; k < selectedLevel.bricks.Count; k++)
                {
                    if (selectedLevel.bricks[k].pos.x+(int)levelWidth/2== i && selectedLevel.bricks[k].pos.y-(int)levelHeight/2 == -j)
                    {
                        var spriteImage = new Image();
                        spriteImage.scaleMode = ScaleMode.ScaleToFit;
                        spriteImage.sprite = selectedLevel.bricks[k].brick.gameObject.GetComponent<SpriteRenderer>().sprite;
                        spriteImage.tintColor = new Color(selectedLevel.bricks[k].data.brickColor.r, selectedLevel.bricks[k].data.brickColor.g, selectedLevel.bricks[k].data.brickColor.b, 1f);
                        holderButton.Add(spriteImage);
                    }
                }
            }
        }

    }

    private void OnHolderClicked()
    {
        var window = new BrickEditorWindow();
        window.ShowModal();


    }

    public class BrickEditorWindow : EditorWindow
    {
        TextField m_ObjectNameBinding;
        
        private void OnEnable()
        {
            var label = new Label("Hello World!");
            
            rootVisualElement.Add(label);

        }
        public void CreateGUI()
        {
            m_ObjectNameBinding = new TextField("Object Name Binding");
            // Note: the "name" property of a GameObject is "m_Name" in serialization.
            m_ObjectNameBinding.bindingPath = "m_Name";
            rootVisualElement.Add(m_ObjectNameBinding);
            OnSelectionChange();
        }

        public void OnSelectionChange()
        {
            GameObject selectedObject = Selection.activeObject as GameObject;
            if (selectedObject != null)
            {
                // Create the SerializedObject from the current selection
                SerializedObject so = new SerializedObject(selectedObject);
                // Bind it to the root of the hierarchy. It will find the right object to bind to.
                rootVisualElement.Bind(so);

                // Alternatively you can instead bind it to the TextField itself.
                // m_ObjectNameBinding.Bind(so);
            }
            else
            {
                // Unbind the object from the actual visual element that was bound.
                rootVisualElement.Unbind();
                // If you bound the TextField itself, you'd do this instead:
                // m_ObjectNameBinding.Unbind();

                // Clear the TextField after the binding is removed
                m_ObjectNameBinding.value = "";
            }
        }
    }
}
