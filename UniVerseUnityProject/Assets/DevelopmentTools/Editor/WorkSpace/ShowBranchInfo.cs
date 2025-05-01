using DevelopmentTools.CustomToolbar;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DevelopmentTools.WorkSpace
{
    public class ShowBranchInfo : CustomToolbarItem
    {
        private const string IconAssetPath = "Icons/icon_git.png";

        private Label label;

        public ShowBranchInfo()
            : base(0, LayoutPositionType.Right)
        {
        }

        public override void OnCreateElement(VisualElement visualElement)
        {
            var root = new VisualElement()
            {
                style =
                {
                    flexShrink = 1,
                    flexDirection = FlexDirection.Row,
                    backgroundColor = EditorGUIUtility.isProSkin
                        ? new Color(0.2f, 0.2f, 0.2f, 1.0f)
                        : new Color(0.8f, 0.8f, 0.8f, 1.0f),
                    borderLeftWidth = 2, borderRightWidth = 2,
                    paddingLeft = 2, paddingRight = 2,
                    minHeight = 20
                }
            };
            
            // Assets/Editor Default Resources/Icon_git.png に,テクスチャがあれば表示する
            var icon = EditorGUIUtility.Load("icon_git.png") as Texture;
            if (icon != null)
            {
                var image = new Image()
                {
                    image = icon,
                    style = { width = 16, height = 16, marginRight = 2 }
                };
                root.Add(image);
            }

            label = new Label
            {
                text = string.Empty,
                style =
                {
                    color = EditorGUIUtility.isProSkin
                        ? new Color(0.9f, 0.9f, 0.9f, 1.0f)
                        : new Color(0.1f, 0.1f, 0.1f, 1.0f),
                    unityTextAlign = TextAnchor.MiddleCenter
                }
            };
            root.Add(label);
            visualElement.Add(root);
            Refresh();
        }

        public override void OnFocusChanged(bool isFocused)
        {
            if (isFocused)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            if (label == null)
                return;

            var branchName = GetBranch();
            if (label != null)
            {
                label.text = branchName;
            }
        }

        private static string GetBranch()
        {
            var gitDirPath = string.Empty;
            var path = Application.dataPath;
            // 3ディレクトリたどって無ければ諦める
            for (var i = 0; i < 3; i++)
            {
                if (string.IsNullOrEmpty(path))
                    break;

                var dirs = System.IO.Directory.GetDirectories(path, ".git");
                if (dirs.Length > 0)
                {
                    gitDirPath = dirs[0];
                    break;
                }
                // ディレクトリを一つ上げる
                path = System.IO.Path.GetDirectoryName(path);
            }

            if (string.IsNullOrEmpty(gitDirPath))
                return string.Empty;

            var headPath = gitDirPath + "/HEAD";
            using var reader = new System.IO.StreamReader(headPath);
            var refs = reader.ReadLine();

            if (string.IsNullOrEmpty(refs) || !refs.StartsWith("ref: refs/heads/"))
                return string.Empty;

            var branch = refs["ref: refs/heads/".Length..];
            return branch;
        }
    }
}