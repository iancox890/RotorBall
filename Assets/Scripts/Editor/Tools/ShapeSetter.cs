using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom inspector for BrickShape.
    /// </summary>
    public static class ShapeSetter
    {
        //Main path
        private const string Path = "GameObject/Psychedelic Games/Shapes/";

        //Co-paths (make sure it is const)
        //Example:
        //private const string CirclePath = Path + "Circle/";
        //Then when you make a menu item... Use (CirclePath + ShapeName)

        //Sub-Paths
        private const string CrossPath = Path + "Cross/";
        private const string CurvePath = Path + "Curve/";
        private const string KitePath = Path + "Kite/";
        private const string ParallelogramPath = Path + "Parallelogram/";
        private const string TrapeziumPath = Path + "Trapezium/";
        private const string TrianglePath = Path + "Triangle/";

        //Individual shape identifiers (the string must be the same name as the shape prefab)
        private const string Circle = "Circle";
        private const string CrossVariation1 = "Cross - Variation 1";
        private const string CrossVariation2 = "Cross - Variation 2";
        private const string CrossVariation3 = "Cross - Variation 3";
        private const string Curve10Variation1 = "Curve - 10 Degrees - Variation 1 Brick Prefab";
        private const string Curve90Variation1 = "Curve - 90 Degrees - Variation 1 Brick Prefab";
        private const string Curve90Solid = "Curve - 90 Degrees - Solid Brick Prefab";
        private const string Decagon = "Decagon";
        private const string Diamond = "Diamond";
        private const string Heart = "Heart";
        private const string Heptagon = "Heptagon";
        private const string Hexagon = "Hexagon";
        private const string KiteVariation1 = "Kite - Variation 1";
        private const string Nonagon = "Nonagon";
        private const string ParallelogramVariation1 = "Parallelogram - Variation 1";
        private const string ParallelogramVariation2 = "Parallelogram - Variation 2";
        private const string Pentagon = "Pentagon";
        private const string Semicircle = "Semicircle";
        private const string Square = "Square";
        private const string Star = "Star";
        private const string TrapeziumVariation1 = "Trapezium - Variation 1";
        private const string TrapeziumVariation2 = "Trapezium - Variation 2";
        private const string TriangleEquilateral = "Triangle - Equilateral";
        private const string TriangleEquilateralHollow = "Triangle - Equilateral - Hollow";
        private const string TriangleRightAngled = "Triangle - Right Angled";

        private const string EquilateralTriangle = "Equilateral Triangle";
        private const string RightTriangle = "Right Triangle";

        private static GameObject[] shapes;
        private static string[] shapeNames;
        private static int shapeLength;

        [InitializeOnLoadMethod]
        private static void Init()
        {
            shapeNames = AssetDatabase.FindAssets("t:GameObject", new string[] { "Assets/Prefabs/Brick Prefabs/Brick Shapes" });
            shapeLength = shapeNames.Length;

            shapes = new GameObject[shapeLength];

            for (int i = 0; i < shapeLength; i++)
            {
                shapes[i] = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(shapeNames[i]));
                shapeNames[i] = shapes[i].name;
            }
        }

        [MenuItem(Path + Circle)]
        private static void SetCircle() => SetShape(Circle);
        [MenuItem(CrossPath + CrossVariation1)]
        private static void SetCrossVariation1() => SetShape(CrossVariation1);
        [MenuItem(CrossPath + CrossVariation2)]
        private static void SetCrossVariation2() => SetShape(CrossVariation2);
        [MenuItem(CrossPath + CrossVariation3)]
        private static void SetCrossVariation3() => SetShape(CrossVariation3);
        [MenuItem(CurvePath + Curve10Variation1)]
        private static void SetCurve10Variation1() => SetShape(Curve10Variation1);  
        [MenuItem(CurvePath + Curve90Variation1)]
        private static void SetCurve90Variation1() => SetShape(Curve90Variation1);
        [MenuItem(CurvePath + Curve90Solid)]
        private static void SetCurve90Solid() => SetShape(Curve90Solid);
        [MenuItem(Path + Decagon)]
        private static void SetDecagon() => SetShape(Decagon);
        [MenuItem(Path + Diamond)]
        private static void SetDiamond() => SetShape(Diamond);
        [MenuItem(Path + Heart)]
        private static void SetHeart() => SetShape(Heart);
        [MenuItem(Path + Heptagon)]
        private static void SetHeptagon() => SetShape(Heptagon);
        [MenuItem(Path + Hexagon)]
        private static void SetHexagon() => SetShape(Hexagon);
        [MenuItem(KitePath + KiteVariation1)]
        private static void SetKiteVariation1() => SetShape(KiteVariation1);
        [MenuItem(Path + Nonagon)]
        private static void SetNonagon() => SetShape(Nonagon);
        [MenuItem(ParallelogramPath + ParallelogramVariation1)]
        private static void SetParallelogramVariation1() => SetShape(ParallelogramVariation1);
        [MenuItem(ParallelogramPath + ParallelogramVariation2)]
        private static void SetParallelogramVariation2() => SetShape(ParallelogramVariation2);
        [MenuItem(Path + Pentagon)]
        private static void SetPentagon() => SetShape(Pentagon);
        [MenuItem(Path + Semicircle)]
        private static void SetSemicircle() => SetShape(Semicircle);
        [MenuItem(Path + Square)]
        private static void SetSquare() => SetShape(Square);
        [MenuItem(Path + Star)]
        private static void SetStar() => SetShape(Star);
        [MenuItem(TrapeziumPath + TrapeziumVariation1)]
        private static void SetTrapeziumVariation1() => SetShape(TrapeziumVariation1);
        [MenuItem(TrapeziumPath + TrapeziumVariation2)]
        private static void SetTrapeziumVariation2() => SetShape(TrapeziumVariation2);
        [MenuItem(TrianglePath + TriangleEquilateral)]
        private static void SetTriangleEquilateral() => SetShape(TriangleEquilateral);
        [MenuItem(TrianglePath + TriangleEquilateralHollow)]
        private static void SetTriangleEquilateralHollow() => SetShape(TriangleEquilateralHollow);
        [MenuItem(TrianglePath + TriangleRightAngled)]
        private static void SetTriangleRightAngled() => SetShape(TriangleRightAngled);

        private static void SetShape(string key)
        {
            GameObject[] targets = Selection.gameObjects;
            Transform shape = null;

            for (int i = 0; i < shapeLength; i++)
            {
                if (shapes[i].name.Contains(key))
                {
                    shape = shapes[i].transform;
                }
            }

            int length = targets.Length;

            //Search through the selection
            for (int i = 0; i < length; i++)
            {
                //Get the brick base and body
                Transform brick = targets[i].transform;
                Transform body = brick.childCount > 0 ? brick.GetChild(0) : null;

                if (body && body.name.Contains("Body"))
                {
                    //Set the sprite for the body and the core body if one exists
                    body.GetComponent<SpriteRenderer>().sprite = shape?.GetComponent<SpriteRenderer>().sprite;
                    Transform coreChild = body.childCount > 0 ? body.GetChild(0) : null;
                    if (coreChild)
                    {
                        coreChild.GetComponent<SpriteRenderer>().sprite = shape?.GetComponent<SpriteRenderer>().sprite;
                    }

                    int children = brick.childCount;

                    //Search for a hollow shape and set the sprite if found
                    for (int j = 0; j < children; j++)
                    {
                        Transform child = brick.GetChild(j);
                        if (child.name.Contains("Hollow"))
                        {
                            child.GetComponent<SpriteRenderer>().sprite = shape?.GetChild(0)?.GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                    ComponentUtility.AddComponent<Collider2D>(shape.gameObject, body.gameObject);
                }
                else
                {
                    Debug.Log("No body found! Is this game object a brick?");
                }
            }
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
