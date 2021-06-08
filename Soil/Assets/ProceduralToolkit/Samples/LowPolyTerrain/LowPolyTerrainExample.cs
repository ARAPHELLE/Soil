using ProceduralToolkit.Samples.UI;
using UnityEngine;

namespace ProceduralToolkit.Samples
{
    /// <summary>
    /// Configurator for LowPolyTerrainGenerator with UI and editor controls
    /// </summary>
    public class LowPolyTerrainExample : ConfiguratorBase
    {
        public LayerMask ableToPutStructuresOn;

        public MeshFilter terrainMeshFilter;
        public MeshCollider terrainMeshCollider;
        public RectTransform leftPanel;
        public bool constantSeed = false;
        public LowPolyTerrainGenerator.Config config = new LowPolyTerrainGenerator.Config();

        private const int minYSize = 1;
        private const int maxYSize = 10;
        private const float minCellSize = 0.3f;
        private const float maxCellSize = 1;
        private const int minNoiseFrequency = 1;
        private const int maxNoiseFrequency = 8;

        public GameObject tree;
        public GameObject rock;
        public GameObject[] clusters;
        public GameObject player;

        private Mesh terrainMesh;

        private void Awake()
        {
            Generate();
            SetupSkyboxAndPalette();

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Terrain height", minYSize, maxYSize, (int) config.terrainSize.y, value =>
                {
                    config.terrainSize.y = value;
                    Generate();
                });

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Cell size", minCellSize, maxCellSize, config.cellSize, value =>
                {
                    config.cellSize = value;
                    Generate();
                });

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Noise frequency", minNoiseFrequency, maxNoiseFrequency, (int) config.noiseFrequency, value =>
                {
                    config.noiseFrequency = value;
                    Generate();
                });

            InstantiateControl<ButtonControl>(leftPanel).Initialize("Generate", () => Generate());
        }

        private void Start()
        {
            Vector3 startPos = RandomPointAboveTerrain();

            RaycastHit hit;
            if (Physics.Raycast(startPos, Vector3.down, out hit, Mathf.Infinity, ableToPutStructuresOn))
            {
                Instantiate(player, position: hit.point, Quaternion.identity);
            }
        }

        private void Update()
        {
            //UpdateSkybox();
        }

        public void Generate(bool randomizeConfig = true)
        {
            if (constantSeed)
            {
                Random.InitState(0);
            }

            if (randomizeConfig)
            {
                GeneratePalette();

                config.gradient = ColorE.Gradient(from: GetMainColorHSV(), to: GetSecondaryColorHSV());
            }

            var draft = LowPolyTerrainGenerator.TerrainDraft(config);
            draft.Move(Vector3.left*config.terrainSize.x/2 + Vector3.back*config.terrainSize.z/2);
            AssignDraftToMeshFilter(draft, terrainMeshFilter, ref terrainMesh);
            terrainMeshCollider.sharedMesh = terrainMesh;

            for (int i = 0; i < 600; i++)
            {
                Vector3 startPos = RandomPointAboveTerrain();

                RaycastHit hit;
                if (Physics.Raycast(startPos, Vector3.down, out hit, Mathf.Infinity, ableToPutStructuresOn))
                {
                    Instantiate(tree, position: hit.point, Quaternion.identity);
                }
            }

            for (int i = 0; i < 200; i++)
            {
                Vector3 startPos = RandomPointAboveTerrain();

                RaycastHit hit;
                if (Physics.Raycast(startPos, Vector3.down, out hit, Mathf.Infinity, ableToPutStructuresOn))
                {
                    Instantiate(clusters[Random.Range(0, clusters.Length)], position: hit.point, Quaternion.identity);
                }
            }

            for (int i = 0; i < 700; i++)
            {
                Vector3 startPos = RandomPointAboveTerrain();

                RaycastHit hit;
                if (Physics.Raycast(startPos, Vector3.down, out hit, Mathf.Infinity, ableToPutStructuresOn))
                {
                    GameObject newRock = Instantiate(rock, position: hit.point, Quaternion.identity);

                    Vector3 newUp = hit.normal;
                    Vector3 oldForward = newRock.transform.forward;

                    Vector3 newRight = Vector3.Cross(newUp, oldForward);
                    Vector3 newForward = Vector3.Cross(newRight, newUp);

                    newRock.transform.rotation = Quaternion.LookRotation(newForward, newUp);
                }
            }
        }

        private Vector3 RandomPointAboveTerrain()
        {
            return new Vector3(
                Random.Range(transform.position.x - config.terrainSize.x / 2, transform.position.x + config.terrainSize.x / 2),
                transform.position.y + config.terrainSize.y * 2,
                Random.Range(transform.position.z - config.terrainSize.z / 2, transform.position.z + config.terrainSize.z / 2)
            );
        }
    }
}
