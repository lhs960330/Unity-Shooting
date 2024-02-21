/* ������ ���� Object pool
 */

/* <������Ʈ Ǯ>
 * 
 * ���α׷� ������ ����ϰ� ��Ȱ��Ǵ� ���� ���� �ν��Ͻ����� ����, ������ �����ʰ�
 * �̸� �����س��� �ν��Ͻ����� ������Ʈ Ǯ�� �����ϰ�
 * �ν��Ͻ��� �뿩, �ݳ��ϴ� ������� ����ϴ� ���
 * 
 * ����
 * 1. �ν��Ͻ����� ������ ������Ʈ Ǯ�� ����
 * 2. ���α׷�(��)�� ���۽� ������Ʈ Ǯ�� �ν��Ͻ����� �����Ͽ� ����
 * 3. �ν��Ͻ��� �ʿ�� �ϴ� ��Ȳ���� ���� ��� ������Ʈ Ǯ���� �ν��Ͻ��� �뿩�Ͽ� ���
 * 4. �ν��Ͻ��� �ʿ�� ���� �ʴ� ��Ȳ���� ���� ��� ������Ʈ Ǯ�� �ݳ��Ͽ� ����
 * 
 * ����
 * 1. ����ϰ� ����ϴ� �ν��Ͻ��� ������ �ҿ�Ǵ� ������带 ����
 * 2. ����ϰ� ����ϴ� �ν��Ͻ��� ������ ������ �÷����� �δ��� ����
 * (������ ���ٰ� �͸������� �����������)
 * 
 * ����(������)
 * 1. �̸� �����س��� �ν��Ͻ��� ���� ������� �ʴ� ��쿡�� �޸𸮸� �����ϰ� ���� (�̸� ����� ���� �����ϱ� ������ �ʿ� ���� ��쿡�� �����س��� �־� �޸𸮸� �����)
 * 2. �޸𸮰� �˳����� ���� ��Ȳ���� �ʹ� ���� ������Ʈ Ǯ���� �����ϴ� ���,
 *    �� ������ ���������� �پ��� ������ ������ �÷��Ϳ� �δ��� �־� ���α׷��� �������� ��츦 ����
 * 
 */

using System.Collections.Generic;
using UnityEngine;

namespace Digin
{
    public class ObjectPooler : MonoBehaviour
    {
        private PooledObject prefab;
        // �����ϱ����� Stack�̳� Queue�� ������ش�. (���� ����)
        private Stack<PooledObject> ObjectPool;
        // ��� �������� ���صд�.
        private int poolSize = 100;

        // �������ִ� �Լ�
        public void CreatePool()
        {
            ObjectPool = new Stack<PooledObject>(poolSize);
            for (int i = 0; i < poolSize; i++)
            {
                PooledObject instance = Instantiate(prefab);
                // ��Ȱ��ȭ �ؼ� �����س��´�. (Ȱ��ȭ�ؼ� ������ ���Ӿ����� Ȱ�����̴� �̷� ���� �� ������ ����.)
                instance.gameObject.SetActive(false);
                ObjectPool.Push(instance);
            }
        }

        // �����ִ� �Լ�
        public PooledObject GetPool()
        {
            // ������ �ٶ� Ȱ��ȭ ���ش�.

            if (ObjectPool.Count > 0)
            {
                PooledObject instance = ObjectPool.Pop();
                instance.gameObject.SetActive(true);
                return instance;
            }
            // ���� ���� ũ�� �̻��̸� �� ������ش�.
            else
            {
                return Instantiate(prefab);
            }
        }

        // �ݳ����ִ� �Լ�
        public void ReturnPool(PooledObject instance)
        {
            // �ݳ��Ҷ��� ��Ȱ��ȭ �����ְ� �־��ش�.

            if (ObjectPool.Count > poolSize)
            {
                instance.gameObject.SetActive(false);
                ObjectPool.Push(instance);
            }
            // ���� ���� ũ�� �̻����� �����Ǿ����� ����
            else
            {
                Destroy(instance);
            }
        }
    }

    public class PooledObject : MonoBehaviour
    {
        // ����, ������ ����� Ŭ����
    }
}